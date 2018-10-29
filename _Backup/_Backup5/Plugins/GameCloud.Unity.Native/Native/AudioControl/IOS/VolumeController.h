//
//  VolumeControler.h
//  Unity-iPhone
//
//  Created by lion on 16/3/30.
//
//

#ifndef VolumeControler_h
#define VolumeControler_h


#endif /* VolumeControler_h */
@interface VolumeController

-(void) getMusicVolumeController;
-(void) setMusicSilence;
-(void) cancelMusicSilence;
-(int) getIsSilence;
-(void) setMusicMax;
-(void) cancelMusicMax;
-(int) getIsMaxVolume;
-(void) setCurrentMusicVolume:(float) current_muiscvolume;
-(float) getCurrentMusicVolume;
@end