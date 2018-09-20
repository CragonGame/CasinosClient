// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // 场景中的地图网格
    public class EbGrid
    {
        //---------------------------------------------------------------------
        EntityMgr mEntityMgr;
        IEbPixelLoader mPixelLoader;
        EbGridRegion[,] mGridRegions;
        EbPixelNav mPixelNav;

        //---------------------------------------------------------------------
        public int WidthGrid { get; private set; }
        public int HeightGrid { get; private set; }
        public float XMinWorld { get; private set; }
        public float ZMinWorld { get; private set; }
        public float WidthWorld { get; private set; }
        public float HeightWorld { get; private set; }

        //---------------------------------------------------------------------
        public EbGrid(EntityMgr entity_mgr)
        {
            mEntityMgr = entity_mgr;
        }

        //---------------------------------------------------------------------
        public EbGridRegion this[int x, int y]
        {
            get { return mGridRegions[x, y]; }
        }

        //---------------------------------------------------------------------
        // unity3d top 视图，坐标原点Terrain左下，而png原点在左上，应用层将坐标转换好传进来
        public void load(IEbPixelLoader pixel_loader, float xmin_world, float zmin_world,
            float width_world, float height_world)
        {
            mPixelLoader = pixel_loader;

            WidthWorld = width_world;
            HeightWorld = height_world;
            XMinWorld = xmin_world;
            ZMinWorld = zmin_world;
            WidthGrid = mPixelLoader.width;
            HeightGrid = mPixelLoader.height;

            mGridRegions = new EbGridRegion[WidthGrid, HeightGrid];
            mPixelNav = new EbPixelNav((uint)(WidthGrid * HeightGrid));

            mPixelNav.blockStart(mPixelLoader.width, mPixelLoader.height);
            mPixelLoader.foreachPixel(
            (bool is_block, int w, int h) =>
            {
                if (is_block)
                {
                    mGridRegions[w, h] = null;
                    mPixelNav.setBlock(w, h);
                }
                else
                {
                    mGridRegions[w, h] = new EbGridRegion(mEntityMgr);
                }
            }
            );
            mPixelNav.blockEnd();
        }

        //---------------------------------------------------------------------
        public List<EbVector3> navigate(EbVector3 from_world, EbVector3 to_world, uint max_step)
        {
            EbVector2 from_grid = _world2gird(from_world);
            EbVector2 to_grid = _world2gird(to_world);

            List<EbVector3> route_points = new List<EbVector3>();
            if (_isInGrid(from_grid) && _isInGrid(to_grid))
            {
                if (mPixelNav.search((int)from_grid.x, (int)from_grid.y, (int)to_grid.x, (int)to_grid.y, max_step))
                {
                    for (int i = 1; i < mPixelNav.Route.Count - 1; ++i)
                    {
                        route_points.Add(_grid2world(mPixelNav.Route[i]));
                    }
                    route_points.Add(to_world);
                }
                else
                {
                    for (int i = 1; i < mPixelNav.Route.Count; ++i)
                    {
                        route_points.Add(_grid2world(mPixelNav.Route[i]));
                    }
                }
            }

            return route_points;
        }

        //---------------------------------------------------------------------
        public bool isInGrid(EbVector3 pos_world)
        {
            return _isInGrid(_world2gird(pos_world));
        }

        //---------------------------------------------------------------------
        public HashSet<EbGridRegion> getGridRegions(EbVector3 min_world, EbVector3 max_world)
        {
            EbVector2 min_gird = _world2gird(min_world);
            EbVector2 max_gird = _world2gird(max_world);

            HashSet<EbGridRegion> regions = new HashSet<EbGridRegion>();

            for (int h = (int)min_gird.y; h <= (int)max_gird.y; h++)
            {
                for (int w = (int)min_gird.x; w < (int)max_gird.x; w++)
                {
                    regions.Add(mGridRegions[w, h]);
                }
            }
            return regions;
        }

        //---------------------------------------------------------------------
        public HashSet<EbGridRegion> getGridRegions(EbBoundingBox bb_world)
        {
            return getGridRegions(bb_world.Min, bb_world.Max);
        }

        //---------------------------------------------------------------------
        public void entityEnterRegion(Entity entity, EbVector3 pos_world)
        {
            EbVector2 pos_grid = _world2gird(pos_world);
            EbGridRegion region = mGridRegions[(int)pos_grid.x, (int)pos_grid.y];
            if (region == null) return;

            region.entityEnterRegion(entity);
        }

        //---------------------------------------------------------------------
        public void entityMove(Entity entity, EbVector3 pos_world)
        {
            EbVector2 pos_grid = _world2gird(pos_world);
            EbGridRegion region = mGridRegions[(int)pos_grid.x, (int)pos_grid.y];
            if (region == null) return;

            EbGridRegion region_last = entity.getUserData<EbGridRegion>();
            if (region.Equals(region_last))
            {
                region.entityMove(entity);
            }
            else
            {
                if (region_last != null) region_last.entityLeaveRegion(entity);
                region.entityEnterRegion(entity);
            }
        }

        //---------------------------------------------------------------------
        public void entityLeaveRegion(Entity entity, EbVector3 pos_world)
        {
            EbVector2 pos_grid = _world2gird(pos_world);
            EbGridRegion region = mGridRegions[(int)pos_grid.x, (int)pos_grid.y];
            if (region == null) return;

            region.entityLeaveRegion(entity);

        }

        //---------------------------------------------------------------------
        bool _isInGrid(EbVector2 pos_grid)
        {
            return (0 <= pos_grid.x && pos_grid.x < mPixelNav.SizeX)
                && (0 <= pos_grid.y && pos_grid.y < mPixelNav.SizeY);
        }

        //---------------------------------------------------------------------
        EbVector2 _world2gird(EbVector3 pos_world)
        {
            EbVector2 v = new EbVector2(pos_world.x, pos_world.z);

            v.x -= XMinWorld;
            v.y = ZMinWorld - v.y;

            v.x = (v.x / WidthWorld) * (float)WidthGrid;
            v.y = (v.y / HeightWorld) * (float)HeightGrid;

            return v;
        }

        //---------------------------------------------------------------------
        EbVector3 _grid2world(EbVector2 pos_grid)
        {
            EbVector3 v = new EbVector3(pos_grid.x, 0, pos_grid.y);

            v.x = (v.x / (float)WidthGrid) * WidthWorld;
            v.z = (v.z / (float)HeightGrid) * HeightWorld;

            v.x += XMinWorld;
            v.z = ZMinWorld - v.z;

            return v;
        }
    }
}
