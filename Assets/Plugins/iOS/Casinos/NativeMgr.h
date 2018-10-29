//
//  NativeMgr.h
//  Unity-iPhone
//
//  Created by yong li on 2017/5/23.
//
//

#ifndef NativeMgr_h
#define NativeMgr_h


#endif /* NativeMgr_h */

@interface NativeMgr : NSObject
@property (nonatomic) NSString* mNativeOperate;
+(instancetype)Instance;
-(NSString*)getCurrentNativeOperate;
-(void)nativeOperate:(const char*)operate_type;
@end
