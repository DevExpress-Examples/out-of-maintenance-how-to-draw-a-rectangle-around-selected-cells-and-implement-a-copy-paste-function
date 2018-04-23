# How to draw a rectangle around selected cells and implement a copy/paste functionality


<p>Unlike the Excel sheet, the XtraGrid doesn't provide a copy/paste functionality, because it displays data of different types and therefore. So, this functionality can't be used in common situations. This example demonstrates how to implement a copy/paste functionality manually, assuming that all data is of the String type. Also, this example demonstrates how to draw a blinking rectangle around the selected area using the GridView.CustomDrawCell event. The CellSelectionHelper component, created within this example, can be simply placed on the form in the VS designer. All you need to make it work is to assign an appropriate GridView to it.</p>


<h3>Description</h3>

Starting with version 17.2, pasting data from the Clipboard is supported out of the box. To enable this feature, set the&nbsp;<strong>GridView.OptionsClipboard.PasteMode</strong>&nbsp;property to Update or Append. It should be possible to paste data by pressing CTRL+V or using the&nbsp;<strong>GridView.PasteFromClipboard</strong>&nbsp;method.

<br/>


