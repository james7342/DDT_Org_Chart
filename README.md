

# DDT_Org_Chart
DNN Org Chart Module

This module was migrated from CodePlex Archive.
It was orginally develoepd by DevPCI for DNN 6.2.4+.

I have taken the orginal source code and have gotten the module working in DNN 9.2.2. There are a few things I'll be trying to work out in the near future. I have not tested this module outside of DNN 9.2.2. I think might be ok with DNN 8.X and above.

I'm no developer by any means so really looking for someone to contribute and help make this module better for teh DNN Community.

# Version Beta 00.00.06
<li>Changes for DNN 9.2.2</li>
<li>Removed DotNetNuke.Services.Search Namespace. Depreciated in 7.1.</li>
<li>Change UserController.GetCurrentUserInfo to UserController.Instance.GetCurrentUserInfo. Depreciated in DNN 7.3.</li>
<li>Change SQL Provider files to check if tables already exist and not try to recreate them during installation.</li>

<hr>

# Original Release Information from CodePlex:
https://archive.codeplex.com/?p=ddtorgchart

About the DevPCI DDT_Org_Chart for DotNetNuke 6.2.4 +

# Version Beta 00.00.05

Implement IPortable (export / import / copy module)

# Version Beta 00.00.04

Fix and Enhancements:

<li>Fix file picker in Edit mode with folder different than root</li>
<li>Localize javascript alert confirm</li>
<li>Localize grids header columns</li>
<li>Localize title (Groups, Items) in Edit</li>

# Version Beta 00.00.03
  <li>Initial Version</li>

# Introduction:

This DevPCI module - DDT_Org_Chart for DotNetNuke 6.2.4 + is based on Telerik Org Chart for the View. The Edit Data part is based on Telerik Grid(s).
<a href>https://www.telerik.com/products/aspnet-ajax/orgchart.aspx</a>
<a href>https://www.telerik.com/products/aspnet-ajax/grid.aspx</a>

# This module has 2 modes of operation:
A "Simple" one, where items are linked to each others (parent / child relation).</br>
A "With Group" one, where group are linked to each others (parent / child relation), and items are linked to groups.</br>

# Please note:
The relations between items and/or groups will be reset by changing mode (Simple vs With Group) in the Settings.</br>
You'll have to rebuild the news relations in the Edit Data part.</br>
Images must be 48px x 48px.</br>
In "Simple" mode, it must have at least one root item (no parent set).</br>
In "With Group" mode, it must have at least one root group (no parent set).</br>
The Drag and Drop will work for admins in the View as soon as: 
<ol>
  <li>There is only one root Item "Simple" mode or only one root Group (With Group mode)</li>
  <li>At least on item has a parent set in "Simple" mode, and at least one group has a parent in "With Group mode"</li>
  <li>There is enought groups and/or items to get sources and destinations for Drag and Drop</li>
</ol> 

# See Demo on Live Website:
http://www.devpci.com/DotNetNuke/Modules/ModuleDDT_Org_Chart/SimpleDemo.aspx
http://www.devpci.com/DotNetNuke/Modules/ModuleDDT_Org_Chart/WithGroupDemo.aspx
http://www.devpci.com/DotNetNuke/Modules/ModuleDDT_Org_Chart/FootballDemo.aspx
 

See 2 demos of content creation and render with drag and drop:</br>
2 min speed up "Simple" demo here: http://youtu.be/orBM1ja_ROM</br>
2 min speed up "With Group" demo here: http://youtu.be/4M1UQz84FHQ</br>

# Known Bug, workaround:
<li>In the Edit Data part, in View Mode of the grid, there is a broken links if images are not set (normal)</li>
<li>Item Title and Group Title works on DNN 6.2.4/5/6, but not on DNN 7.0.5+ (Have to investigate on it, probably something with Telerik DLL more than DNN</li>
<li>Setting Reduce Size of 25 % will impact all Org Chart of a page if many
