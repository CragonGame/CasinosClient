// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class ControllerLogin : Controller
    {
        //---------------------------------------------------------------------
        LoginPlayerPrefs LoginPlayerPrefs { get; set; }

        //---------------------------------------------------------------------
        public override void Create()
        {
            Debug.Log("ControllerLogin.Create()");

            var path_mgr = Context.Instance.PathMgr;
            var tb_mgr = Context.Instance.EbDataMgr;

            Context.Instance.SoundMgr.Play("background", _eSoundLayer.Background);

            byte[] bytes_common = System.IO.File.ReadAllBytes(path_mgr.DirCommonRoot + "Raw/TbData/KingCommon.db");
            byte[] bytes_client = System.IO.File.ReadAllBytes(path_mgr.DirCommonRoot + "Raw/TbData/KingClient.db");
            byte[] bytes_desktop = System.IO.File.ReadAllBytes(path_mgr.DirCommonRoot + "Raw/TbData/KingDesktop.db");
            byte[] bytes_desktoph = System.IO.File.ReadAllBytes(path_mgr.DirCommonRoot + "Raw/TbData/KingDesktopH.db");

            tb_mgr.LoadAllTableFromMemoryDb("KingCommon", bytes_common);
            tb_mgr.LoadAllTableFromMemoryDb("KingClient", bytes_client);
            tb_mgr.LoadAllTableFromMemoryDb("KingDesktop", bytes_desktop);
            tb_mgr.LoadAllTableFromMemoryDb("KingDesktopH", bytes_desktoph);

            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopInfoTexas>(Casinos.TableName.DesktopInfoTexas.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataExpression>(Casinos.TableName.Expression.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopInfoGFlower>(Casinos.TableName.DesktopInfoGFlower.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataConfigGFlowerDesktop>(Casinos.TableName.ConfigGFlowerDesktop.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopGFlowerBetOperate>(Casinos.TableName.DesktopGFlowerBetOperate.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHInfoTexas>(Casinos.TableName.DesktopHInfoTexas.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetOperateTexas>(Casinos.TableName.DesktopHBetOperateTexas.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetPotTexas>(Casinos.TableName.DesktopHBetPotTexas.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigTexasDesktopHSysBank>(Casinos.TableName.CfigTexasDesktopHSysBank.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigTexasDesktopH>(Casinos.TableName.CfigTexasDesktopH.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigTexasDesktopHGoldPercent>(Casinos.TableName.CfigTexasDesktopHGoldPercent.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataRaiseBlind>(Casinos.TableName.TexasRaiseBlinds.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataTexasSnowBallRewardInfo>(Casinos.TableName.TexasSnowBallRewardInfo.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataTexasSnowBallRewardPlayerNum>(Casinos.TableName.TexasSnowBallRewardPlayerNum.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataMatchTexasCreate>(Casinos.TableName.MatchTexasCreate.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHInfoGFlower>(Casinos.TableName.DesktopHInfoGFlower.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetOperateGFlower>(Casinos.TableName.DesktopHBetOperateGFlower.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetPotGFlower>(Casinos.TableName.DesktopHBetPotGFlower.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigGFlowerDesktopHSysBank>(Casinos.TableName.CfigGFlowerDesktopHSysBank.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigGFlowerDesktopH>(Casinos.TableName.CfigGFlowerDesktopH.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigGFlowerDesktopHGoldPercent>(Casinos.TableName.CfigGFlowerDesktopHGoldPercent.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHInfoNiuNiu>(Casinos.TableName.DesktopHInfoNiuNiu.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetOperateNiuNiu>(Casinos.TableName.DesktopHBetOperateNiuNiu.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetPotNiuNiu>(Casinos.TableName.DesktopHBetPotNiuNiu.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigNiuNiuDesktopHSysBank>(Casinos.TableName.CfigNiuNiuDesktopHSysBank.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigNiuNiuDesktopH>(Casinos.TableName.CfigNiuNiuDesktopH.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigNiuNiuDesktopHGoldPercent>(Casinos.TableName.CfigNiuNiuDesktopHGoldPercent.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHInfoZhongFB>(Casinos.TableName.DesktopHInfoZhongFB.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetOperateZhongFB>(Casinos.TableName.DesktopHBetOperateZhongFB.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetPotZhongFB>(Casinos.TableName.DesktopHBetPotZhongFB.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigZhongFBDesktopHSysBank>(Casinos.TableName.CfigZhongFBDesktopHSysBank.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigZhongFBDesktopH>(Casinos.TableName.CfigZhongFBDesktopH.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCfigZhongFBDesktopHGoldPercent>(Casinos.TableName.CfigZhongFBDesktopHGoldPercent.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataLotteryTicket>(Casinos.TableName.LotteryTicket.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataLotteryTicketGoldPercent>(Casinos.TableName.LotteryTicketGoldPercent.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataLotteryTicketBetOperate>(Casinos.TableName.LotteryTicketBetOperate.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataLotteryTicketCardTypePercent>(Casinos.TableName.LotteryTicketCardTypePercent.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataForestPartyDesktop>(Casinos.TableName.ForestPartyDesktop.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataForestPartyBetOperate>(Casinos.TableName.ForestPartyBetOperate.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataForestPartyBetPotMultiple>(Casinos.TableName.ForestPartyBetPotMultiple.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataForestPartyAnimalList>(Casinos.TableName.ForestPartyAnimalList.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataForestPartyLottery>(Casinos.TableName.ForestPartyLottery.ToString());

            //tb_mgr.ParseTableAllData<Casinos.TbDataActorLevel>(Casinos.TableName.ActorLevel.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataCommon>(Casinos.TableName.Common.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDailyReward>(Casinos.TableName.DailyReward.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataOnlineReward>(Casinos.TableName.OnlineReward.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataItem>(Casinos.TableName.Item.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataItemType>(Casinos.TableName.ItemType.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataLanEn>(Casinos.TableName.LanEn.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataLanZh>(Casinos.TableName.LanZh.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitBilling>(Casinos.TableName.UnitBilling.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitConsume>(Casinos.TableName.UnitConsume.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitGiftNormal>(Casinos.TableName.UnitGiftNormal.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitGiftTmp>(Casinos.TableName.UnitGiftTmp.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitGoldPackage>(Casinos.TableName.UnitGoldPackage.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitGoodsVoucher>(Casinos.TableName.UnitGoodsVoucher.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitMagicExpression>(Casinos.TableName.UnitMagicExpression.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataUnitWechatRedEnvelopes>(Casinos.TableName.UnitRedEnvelopes.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataDesktopHBetReward>(Casinos.TableName.DesktopHBetReward.ToString());
            //tb_mgr.ParseTableAllData<Casinos.TbDataVIPLevel>(Casinos.TableName.VIPLevel.ToString());

            LoginPlayerPrefs = new LoginPlayerPrefs();

            ViewMgr.CreateView<ViewLogin>();
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
        }
    }
}