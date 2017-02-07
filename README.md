##VR_View

This is a fork of [ScreenTask](https://github.com/EslaMx7/ScreenTask) in C# by [EslaMx7](https://github.com/EslaMx7)

------------------------------

VR_view is my quick solution for virtual PC desktop on Windows 10 Mobile using cardboard type head mounted displays (HMD).

What it does is show your PC desktop in a side-by-side view. It works by taking a screenshot of your desktop at a configured interval, and displaying it as two side by side images. I find 33ms to be a good interval for most uses, but you can go lower or higher depending on your uses.

It is completely PC based and does not have a UWP companion app. You can use the remote desktop app of your choice to connect to your PC. For higher framerate, for watching videos for example, i recommend game streaming apps instead of normal RDP, as they offer 30fps or better. But for general browser use, reading, etc, normal remote desktop apps offer better clarity.

Now here's the catch, since you need a monitor to run the application and view it, and also a monitor to work on, you have to have two monitors. So you put your browser or whatever you want to view in your HMD on one monitor, and on the second monitor you run VR_View, and then view that monitor with remote desktop or a game/app streamer.

My preferences are [Teamviewer](https://www.microsoft.com/en-us/store/p/teamviewer-remote-control/9wzdncrfj0rh) for clarity, and [Remotr](https://www.microsoft.com/en-us/store/p/remotr/9nblggh2kbv2) for watching video or doing other tasks that require a higher framerate. These are both free apps available for Windows 10 Mobile from the Windows store. Teamviewer is also available for Windows Phone 8.1, but Remotr is not, but there is another app called [KinoConsole](https://www.microsoft.com/en-us/store/p/kinoconsole/9wzdncrdms6r) that is almost as good.

------------------------------

If you don't have two monitors, you can fake it. Windows has long had the ability to "try to connect anyway" to a non-existent monitor, but unfortunately that ability was recently removed from Windows 10. It may still work on previous versions of windows, and Windows 10 pre anniversary update.

For users of Windows 10 post AU, i found a little utility called [spacedesk](http://spacedesk.ph/) that will accomplish the same thing. Basically, you enable the display driver, and then go to a local url in a web browser, and a second monitor is created. It is meant to extend your windows desktop onto another device with a browser, but you can use a browser on the same computer, and just minimize it. With the browser connected, the second monitor will show up in your display settings, where you should set it to extend your desktop instead of clone it.

Another option is to create a ghost monitor using a special hdmi or vga connector, to trick windows into thinking another real monitor is attached.

------------------------------

If you use spacedesk as a second monitor, or have two physical monitors of different resolutions, it is best to run VR_View on the monitor with the higher resolution, to get the clearest view through the remote connection from your phone to the PC.

------------------------------

VR_View also retains ScreenTask's capability to stream the desktop view via web browser, but i have not split that view into a side by side presentation.

------------------------------

> You can download the pre-compiled application [here](https://drive.google.com/file/d/0B6k8Z8ibdu9UUTdXUTBUaDdzQlk/view?usp=sharing).

> .NET Framework 4.5 Required [Download NOW!](http://www.microsoft.com/en-eg/download/details.aspx?id=30653)

> Works On Windows Vista/7/8/10 | Windows XP Not Supported Since The .NET 4.5 Not Supported On It.

------------------------------

### Working view (work on left monitor, view on right): 
![Normal view](https://drive.google.com/file/d/0B6k8Z8ibdu9UNW5qUmp0dUhJMUk/view?usp=sharing)

### Settings view: 
![Settings view](https://drive.google.com/file/d/0B6k8Z8ibdu9UNXBLV3JBbmxGWk0/view?usp=sharing)

### Mobile view through Teamviewer: 
![Mobile view](https://drive.google.com/file/d/0B6k8Z8ibdu9UTnNGV2d0blRfNlk/view?usp=sharing)


