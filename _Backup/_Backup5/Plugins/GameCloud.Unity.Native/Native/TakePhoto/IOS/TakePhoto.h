//
//  TakePhoto.h
//  Unity-iPhone
//
//  Created by lion on 16/3/29.
//
//

#ifndef TakePhoto_h
#define TakePhoto_h


#endif /* TakePhoto_h */

@interface TakePhoto : NSObject<UIApplicationDelegate,UIImagePickerControllerDelegate, UIActionSheetDelegate,UINavigationControllerDelegate>
{
    
    UIView*				_rootView;
    UIViewController*	_rootController;
@private
    id _popoverViewController;
}
@property (nonatomic, retain) id popoverViewController;
@property (nonatomic) NSString* mMsgReciverName;
@property (nonatomic) NSString* mTakePhotoSuccessMsgName;
@property (nonatomic) NSString* mTakePhotoFailMsgName;
@property (nonatomic) int mTakePhotoWidth;
@property (nonatomic) int mTakePhotoHeight;
-(void)setUnityMsgReciverName:(const char*)str;
-(void)setTakePhotoSuccessMsgName:(const char*)str;
-(void)setTakePhotoFailMsgName:(const char*)str;
-(void)setTakePhotoWidth:(int)width;
-(void)setTakePhotoHeight:(int)height;
@end