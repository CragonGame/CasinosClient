//
//  TakePhoto.m
//  Unity-iPhone
//
//  Created by lion on 16/3/29.
//
//

#import "TakePhoto.h"

@implementation TakePhoto
@synthesize popoverViewController = _popoverViewController;
@synthesize mMsgReciverName,mTakePhotoSuccessMsgName,mTakePhotoFailMsgName,mTakePhotoWidth,mTakePhotoHeight;

-(void)setUnityMsgReciverName:(const char*)str
{
    mMsgReciverName = [[NSString alloc] initWithUTF8String:str];
}

-(void)setTakePhotoSuccessMsgName:(const char*)str
{
    mTakePhotoSuccessMsgName = [[NSString alloc] initWithUTF8String:str];
}

-(void)setTakePhotoFailMsgName:(const char*)str
{
    mTakePhotoFailMsgName = [[NSString alloc] initWithUTF8String:str];
}

-(void)setTakePhotoWidth:(int)width
{
    mTakePhotoWidth = width;
}

-(void)setTakePhotoHeight:(int)height
{
    mTakePhotoHeight = height;
}

-(void)showCameraActionSheet
{
   UIActionSheet *sheet = [UIActionSheet alloc];
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad )
        [sheet showFromRect:CGRectMake( 0, 0, mTakePhotoWidth, mTakePhotoHeight ) inView:UnityGetGLViewController().view animated:YES];
    else
        [sheet showInView:UnityGetGLViewController().view];
    
    [sheet release];
    
    [self showPicker:UIImagePickerControllerSourceTypeCamera];
}

-(void)showPictureActionSheet
{
       UIActionSheet *sheet = [UIActionSheet alloc];
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad )
        [sheet showFromRect:CGRectMake( 0, 0, mTakePhotoWidth, mTakePhotoHeight ) inView:UnityGetGLViewController().view animated:YES];
    else
        [sheet showInView:UnityGetGLViewController().view];
    
    [sheet release];
    [self showPicker:UIImagePickerControllerSourceTypePhotoLibrary];
}

- (void)actionSheet:(UIActionSheet*)actionSheet clickedButtonAtIndex:(NSInteger)buttonIndex
{
    if( buttonIndex == 0 )
    {
        [self showPicker:UIImagePickerControllerSourceTypeCamera];
    }
    else if( buttonIndex == 1 )
    {
        [self showPicker:UIImagePickerControllerSourceTypePhotoLibrary];
    }
    else // Cancelled
    {
        //UnityPause( false );
        //UnitySendMessage( "EtceteraManager", "imagePickerDidCancel", "" );
    }
}

- (void)showPicker:(UIImagePickerControllerSourceType)type
{
    UIImagePickerController *picker = [[[UIImagePickerController alloc] init] autorelease];
    picker.delegate = self;
    picker.sourceType = type;
    picker.allowsEditing = YES;
    NSLog( @"showPicker");
    // We need to display this in a popover on iPad
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad )
    {
        Class popoverClass = NSClassFromString( @"UIPopoverController" );
        if( !popoverClass )
            return;
        
        _popoverViewController = [[popoverClass alloc] initWithContentViewController:picker];
        [_popoverViewController setDelegate:self];
        //picker.modalInPopover = YES;
        
        // Display the popover
        [_popoverViewController presentPopoverFromRect:CGRectMake( 0, 0, mTakePhotoWidth, mTakePhotoHeight )
                                                inView:UnityGetGLViewController().view
                              permittedArrowDirections:UIPopoverArrowDirectionAny
                                              animated:YES];
    }
    else
    {
        // wrap and show the modal
        UIViewController *vc = UnityGetGLViewController();
        [vc presentModalViewController:picker animated:YES];
    }
}
- (void)popoverControllerDidDismissPopover:(UIPopoverController*)popoverController
{
    self.popoverViewController = nil;
    //UnityPause( false );
    NSLog( @"popoverControllerDidDismissPopover: cancel" );
    //UnitySendMessage( "EtceteraManager", "imagePickerDidCancel", "" );
    UnitySendMessage([mMsgReciverName UTF8String], [mTakePhotoFailMsgName UTF8String], "");
}

- (void)imagePickerController:(UIImagePickerController*)picker didFinishPickingMediaWithInfo:(NSDictionary*)info
{
    // Grab the image and write it to disk
    UIImage *image;
    UIImage *image2;
    //	if( _pickerAllowsEditing )
    image = [info objectForKey:UIImagePickerControllerEditedImage];
    //        else
    //            image = [info objectForKey:UIImagePickerControllerOriginalImage];
    
    NSLog( @"picker got image with orientation: %i", image.imageOrientation );
    UIGraphicsBeginImageContext(CGSizeMake(mTakePhotoWidth, mTakePhotoHeight));
    [image drawInRect:CGRectMake(0, 0, mTakePhotoWidth, mTakePhotoHeight)];
    image2 = UIGraphicsGetImageFromCurrentImageContext();
    UIGraphicsEndImageContext();
    
    // 得到了image，然后用你的函数传回u3d
    NSData *imgData;
    if(UIImagePNGRepresentation(image2) == nil)
    {
        imgData= UIImageJPEGRepresentation(image2, .6);
    }
    else
    {
        imgData= UIImagePNGRepresentation(image2);
    }
    
    NSString *_encodeImageStr = [imgData base64Encoding];
    NSLog( @"_encodeImageStr: %i", _encodeImageStr );
    UnitySendMessage([mMsgReciverName UTF8String], [mTakePhotoSuccessMsgName UTF8String], [_encodeImageStr UTF8String]);
    
    // Dimiss the pickerController
    [self dismissWrappedController];
}
- (void)imagePickerControllerDidCancel:(UIImagePickerController*)picker
{
    // dismiss the wrapper, unpause and notifiy Unity what happened
    [self dismissWrappedController];
    
    NSLog( @"imagePickerControllerDidCancel: cancel" );
    UnitySendMessage([mMsgReciverName UTF8String], [mTakePhotoFailMsgName UTF8String], "");
    //UnityPause( false );
    //UnitySendMessage( "EtceteraManager", "imagePickerDidCancel", "" );
}

- (void)dismissWrappedController
{
    //UnityPause( false );
    
    UIViewController *vc = UnityGetGLViewController();
    
    // No view controller? Get out of here.
    if( !vc )
        return;
    
    // dismiss the view controller
    [vc dismissModalViewControllerAnimated:YES];
    
    // remove the wrapper view controller
    [self performSelector:@selector(removeAndReleaseViewControllerWrapper) withObject:nil afterDelay:1.0];
    
    //UnitySendMessage( "EtceteraManager", "dismissingViewController", "" );
}

- (void)removeAndReleaseViewControllerWrapper
{
    // iPad might have a popover
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad && _popoverViewController )
    {
        [_popoverViewController dismissPopoverAnimated:YES];
        self.popoverViewController = nil;
    }
}
@end

extern "C" void takeNewPhoto_ios(char* msg_recivername,char* take_photosuccessmsgname,
                             char* take_photofailmsgname,int take_photowidth,int take_photoheight)//xiangji
{
     NSLog( @"takeNewPhoto");
    TakePhoto * app = [[TakePhoto alloc] init];
    
    [app setUnityMsgReciverName:msg_recivername];
    [app setTakePhotoSuccessMsgName:take_photosuccessmsgname];
    [app setTakePhotoFailMsgName:take_photofailmsgname];
    [app setTakePhotoWidth:take_photowidth];
    [app setTakePhotoHeight:take_photoheight];
    [app showCameraActionSheet];
}

extern "C" void takeExistPhoto_ios(char* msg_recivername,char* take_photosuccessmsgname,
                               char* take_photofailmsgname,int take_photowidth,int take_photoheight)//相册
{
    NSLog( @"takeExistPhoto");
    TakePhoto * app = [[TakePhoto alloc] init];
    
    [app setUnityMsgReciverName:msg_recivername];
    [app setTakePhotoSuccessMsgName:take_photosuccessmsgname];
    [app setTakePhotoFailMsgName:take_photofailmsgname];
    [app setTakePhotoWidth:take_photowidth];
    [app setTakePhotoHeight:take_photoheight];
    [app showPictureActionSheet];
    }
