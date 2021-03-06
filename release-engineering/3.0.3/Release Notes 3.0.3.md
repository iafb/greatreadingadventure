## Release Notes: 3.0.3

### Introduction

The Great Reading Adventure is a robust, open source software designed to manage library reading programs. The GRA is free to use, modify, and share. Check out [www.greatreadingadventure.com](http://www.greatreadingadventure.com/) for an overview of its functionality and capabilities.

### Release

These release notes accompany **[:books: The Great Reading Adventure version 3.0.3](https://github.com/MCLD/greatreadingadventure/releases/download/v3.0.3/GreatReadingAdventure-3.0.3.zip)** which can be downloaded from GitHub!

**To upgrade** v3.0.* to this release, please see the **[Upgrade GRA v3.* to v3.0.3 forum post](http://forum.greatreadingadventure.com/t/upgrade-gra-v3-to-v3-0-3/88)**.

We currently **do not** have an upgrade path to go from any version prior to 3.0.0 to this version due to architectural changes. If you have a critical need for the ability to upgrade from v2.*, please [post in the Help category on the forum](http://forum.greatreadingadventure.com/c/help) and we will try to work something out.

### Documentation

For information on what is required to run the Great Reading Adventure, please refer to the [online manual](http://manual.greatreadingadventure.com/).

### Changes in this release

- Fix #83 Patron sub-account list causes error, patron details -> patron tab causes error
- Fix #82 HTML entered in Organization configuration isn't rendered on the selection screen
- Add missing stored procedure app_Notifications_GetAllToOrFromPatron
- Improve patron password recovery logging

### Known issues

These are some of the more significant issues which have already been reported. Feel free to post to [the forum](http://forum.greatreadingadventure.com/) with more information or new issues. GitHub always contains the up-to-date [technical developer view of issues and bug reports](https://github.com/MCLD/greatreadingadventure/issues).

- #23 With JavaScript off, enter on the login page should submit the login form
- #24 Events with no end date do not display and codes for them do not function
- #28 Libraries/Schools don't function unless they are a part of a district
- #29 Prompt user for sysadmin password during configure
- #38 Enter book details panel does not respond to "Enter"
