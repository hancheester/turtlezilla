# TurtleZilla
## Introduction
This project provides an issue tracking plugin (an [IBugTraqProvider](https://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-ibugtraqprovider.html) implementation) for Tortoise SVN for [Bugzilla](https://www.bugzilla.org).

Once this plugin is installed and configured, you will have a new button in TortoiseSVN commit window that will display a list of Bugzilla issues which you can choose to select to include in your commit message.

## Prerequisites
* [Bugzilla](https://www.bugzilla.org)
  * Please make sure you have a WebService API key generated. Information about generating a key can be found at [https://bugzilla.readthedocs.io/en/latest/using/preferences.html#api-keys](https://bugzilla.readthedocs.io/en/latest/using/preferences.html#api-keys).
* [TortoiseSVN](https://tortoisesvn.net)

## Quick Start
##### Installing TurtleZilla
* Please download the installer from [dist](./dist) folder.
* If you prefer to build from source yourself see the [Working With The Source Code section](#work-with-the-source-code) below

##### Configuring TurtleZilla
1. Browse to your folder that contains *repository* which you wish to hook up your Bugzilla issues list.
2. Right-click the folder and go to **TortoiseSVN** -> **Settings**.
3. In the **Settings** dialog, select **Issue Tracking Integration** and click **Add** button.
4. In the **Configure Issue Tracker Integration** dialog, you need to specify three values.
   * Set the *Working Copy Path* to the local repository location.
   * Set the *Provider* to TurtleZilla provider.
   * Set the *Parameters* to "url=[Your Bugzilla URL];product=[Project or Product Name];apikey=[Web Service API Key]"
5. Click *Options* button to test your connection (optional).
6. Click *OK* button to confirm and return to TortoiseSVN window. You should now have the plugin in a list pointing to your [project] path.

##### Using TurtleZilla
1. Bring up the TortoiseSVN commit dialog (by right-click on any files or folder and select *SVN Commit...* on a popup menu"), **Bugzilla Issues** button would appear at the top right corner of the dialog window.
2. CLick the *Bugzilla Issues* button to bring up the TurtleZilla window.
3. Things you could do:
   * A basic search box to search with the issues.
   * A drop down list to choose which field to seach by.
   * A checkbox to include closed issues.
   * Click *OK* button to add selected issue[s] to your commit message, otherwise *Cancel* to return to your commit dialog.
4. An auto generated message will be appended to the end of your existing commit message.

## Work With The Source Code
Under construction...