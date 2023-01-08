
# TrayLamp

Adds an icon to the system tray to mimic a Philips Hue API compatible light controlled by [HueUpdater](https://github.com/jorgeyanesdiez/HueUpdater).

![Stable, Stable & Building, Broken, Broken & Building](https://i.imgur.com/lC3lbyp.jpeg)
Stable | Stable & Building | Broken | Broken & Building






## Motivation

I use a lamp at work to give our team instant feedback about the status of multiple projects tracked by our CI system.

With more and more people working from home, the lamp at the office is not enough to give everyone feedback.

This simple application shows the status of the lamp in the system tray. It was also my excuse to quickly try out Avalonia :wink:






## Usage prerequisites

* Operational *HueUpdater* installation.

* Web access to the *last-status.json* file created by *HueUpdater*.

* Basic JSON knowledge to edit the settings file.






## Deployment

Unpack the release file wherever you want on your system. I suggest *C:\TrayLamp*

Open the *appsettings.json* file with a plain text editor and carefully tweak the values to match your needs.

Here's an attempt to explain each one, although I hope most are self explanatory from the provided sample file.



* ***StatusUrl***

  URL to the *last-status.json* file created and updated by *HueUpdater*

  Example: `https://jenkins-server.mycompany.com/userContent/last-status.json`



* ***DelaySeconds***

  Number of seconds between checks.

  You want to set this to a relatively low value to get fast feedback.

  Default value: 5



* ***TimeoutSeconds***

  Number of seconds after which a check is considered obsolete and is cancelled.

  This value should be lower than *DelaySeconds* but greater than 0.

  Default value: 2






Finally, make the program start when you login.

On Windows, the easiest way is to add a shortcut to your ***%APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup*** folder

That's all there is to it.






## License

Everything under the folder ***TrayLamp/Assets/Icons-Source-Original*** is governed by the free tier of the Freepik - Flaticon license

See the file *TrayLamp/Assets/Icons-Source-Original/flaticon_license.pdf* for the relevant excerpt.

Also see https://www.freepikcompany.com/legal#nav-flaticon for the full Terms and Conditions.

The rest of this project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.






## Credits

Original lamp icon created by Freepik - Flaticon.
Source: https://www.flaticon.com/free-icon/desk-lamp_235602
