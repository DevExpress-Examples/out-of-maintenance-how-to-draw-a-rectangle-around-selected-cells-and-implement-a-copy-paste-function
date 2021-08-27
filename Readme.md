<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128627995/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1603)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CellSelectionHelper.cs](./CS/Q212929/CellSelectionHelper.cs) (VB: [CellSelectionHelper.vb](./VB/Q212929/CellSelectionHelper.vb))
* [Form1.cs](./CS/Q212929/Form1.cs) (VB: [Form1.vb](./VB/Q212929/Form1.vb))
* [Program.cs](./CS/Q212929/Program.cs) (VB: [Program.vb](./VB/Q212929/Program.vb))
<!-- default file list end -->
# How to draw a rectangle around selected cells and implement a copy/paste functionality


<p>Unlike the Excel sheet, the XtraGrid doesn't provide a copy/paste functionality, because it displays data of different types and therefore. So, this functionality can't be used in common situations. This example demonstrates how to implement a copy/paste functionality manually, assuming that all data is of the String type. Also, this example demonstrates how to draw a blinking rectangle around the selected area using the GridView.CustomDrawCell event. The CellSelectionHelper component, created within this example, can be simply placed on the form in the VS designer. All you need to make it work is to assign an appropriate GridView to it.</p>

<br/>


