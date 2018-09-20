//
//  VolumeControler.m
//  Unity-iPhone
//
//  Created by lion on 16/3/30.
//
//

#import <MediaPlayer/MediaPlayer.h>
#import "VolumeController.h"

@implementation VolumeController
UISlider* mVolumeController = nil;
float mCurrentVolume;
bool mIsSilence;
bool mIsMax;


-(void)getMusicVolumeController
{
    MPVolumeView* volume_view = [[MPVolumeView alloc] init];
    volume_view.frame = CGRectMake(-1000, -100, 100, 100);
    if(mVolumeController == nil)
    {
        for (UIView *view in [volume_view subviews]) {
            if ([view.description isEqualToString:@"MPVolumeSlider"]) {
                mVolumeController = (UISlider*)view;
                break;
            }
        }
    }
}

-(void)setMusicSilence
{
    mIsSilence = true;
    mIsMax = false;
    [mVolumeController setValue:0];
}

-(void)cancelMusicSilence
{
    mIsSilence = false;
    [mVolumeController setValue:mCurrentVolume];
}

-(int)getIsSilence
{
    int is_silence = 0;
    if(mIsSilence)
    {
        is_silence = 1;
    }
    
    return is_silence;
}

-(void)setMusicMax
{
    mIsMax = true;
    mIsSilence = false;
    [mVolumeController setValue:1];
}

-(void)cancelMusicMax
{
    mIsMax = false;
    [mVolumeController setValue:mCurrentVolume];
}

-(int)getIsMaxVolume
{
    int is_max = 0;
    if(mIsMax)
    {
        is_max = 1;
    }
    
    return is_max;
}

-(void)setCurrentMusicVolume:(float) current_muiscvolume
{
    MPMusicPlayerController *mpc = [MPMusicPlayerController applicationMusicPlayer];
    mpc.volume = current_muiscvolume;
    
    //mCurrentVolume = current_muiscvolume;
    //[mVolumeController setValue:mCurrentVolume];
}

-(float)getCurrentMusicVolume
{
	MPMusicPlayerController *mpc = [MPMusicPlayerController applicationMusicPlayer];
    return mpc.volume;
}

@end

extern "C" void setMusicSilence_ios()
{
    [VolumeController getMusicVolumeController];
    [VolumeController setMusicSilence];
}

extern "C" void cancelMusicSilence_ios()
{
    [VolumeController getMusicVolumeController];
    [VolumeController cancelMusicSilence];
}

extern "C" int getIsSilence_ios()
{
    return [VolumeController getIsSilence];
}

extern "C" void setMusicMax_ios()
{
    [VolumeController getMusicVolumeController];
    [VolumeController setMusicMax];
}

extern "C" void cancelMusicMax_ios()
{
    [VolumeController getMusicVolumeController];
    [VolumeController cancelMusicMax];
}

extern "C" int getIsMaxVolume_ios()
{
    return [VolumeController getIsMaxVolume];
}

extern "C" void setCurrentMusicVolum_ios(float current_musicvolume)
{
    //[VolumeController getMusicVolumeController];
    [VolumeController setCurrentMusicVolume:current_musicvolume];
}

extern "C" float getCurrentMusicVolume_ios()
{
    return [VolumeController getCurrentMusicVolume];
}
