Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports DevExpress.Data
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.Utils.Drawing

Namespace DXSample
    Public Class CellSelectionHelper
        Inherits Component

        Private disposed As Boolean = False
        Private selectedArea As Rectangle = Rectangle.Empty

        Private evenPicture_Renamed As Bitmap

        Private oddPicture_Renamed As Bitmap
        Private even As Boolean = True
        Private timer As Timer

        Private Const selectionBorderWidth As Integer = 3

        Public Sub New()
            If Not DesignMode Then
                timer = New Timer()
                AddHandler timer.Tick, AddressOf OnTimerTick
                timer.Start()
            End If
            selectedArea.X = Integer.MaxValue
        End Sub

        Private fView As GridView
        Public Property View() As GridView
            Get
                Return fView
            End Get
            Set(ByVal value As GridView)
                If fView Is value Then
                    Return
                End If
                If fView IsNot Nothing Then
                    UnsubscribeViewEvents()
                End If
                fView = value
                If value IsNot Nothing Then
                    SubscribeViewEvents()
                    fView.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update
                End If
            End Set
        End Property

        Public Property EvenPicture() As Bitmap
            Get
                If evenPicture_Renamed Is Nothing Then
                    evenPicture_Renamed = CreateBitmap(True)
                End If
                Return evenPicture_Renamed
            End Get
            Set(ByVal value As Bitmap)
                If evenPicture_Renamed IsNot Nothing AndAlso evenPicture_Renamed.Tag.ToString() = "DXSample" Then
                    evenPicture_Renamed.Dispose()
                End If
                evenPicture_Renamed = value
            End Set
        End Property

        Public Property OddPicture() As Bitmap
            Get
                If oddPicture_Renamed Is Nothing Then
                    oddPicture_Renamed = CreateBitmap(False)
                End If
                Return oddPicture_Renamed
            End Get
            Set(ByVal value As Bitmap)
                If oddPicture_Renamed IsNot Nothing AndAlso oddPicture_Renamed.Tag.ToString() = "DXSample" Then
                    oddPicture_Renamed.Dispose()
                End If
                oddPicture_Renamed = value
            End Set
        End Property

        Private Sub OnGridViewKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            Select Case e.KeyCode
                Case Keys.C
                    If e.Control Then
                        ClearSelection()
                        Dim cells() As GridCell = fView.GetSelectedCells()
                        If cells.Length = 0 Then
                            Return
                        End If
                        Dim minCol, minRow, maxCol, maxRow As Integer
                        minRow = Integer.MaxValue
                        minCol = minRow
                        maxRow = 0
                        maxCol = maxRow
                        For Each cell As GridCell In cells
                            minCol = Math.Min(cell.Column.VisibleIndex, minCol)
                            minRow = Math.Min(cell.RowHandle, minRow)
                            maxCol = Math.Max(cell.Column.VisibleIndex, maxCol)
                            maxRow = Math.Max(cell.RowHandle, maxRow)
                        Next cell
                        If (maxCol - minCol + 1) * (maxRow - minRow + 1) <> cells.Length Then
                            XtraMessageBox.Show("This action won't work on multiple selections.")
                            e.SuppressKeyPress = True
                            Exit Select
                        End If
                        selectedArea = New Rectangle(minCol, minRow, maxCol - minCol, maxRow - minRow)
                    End If
                Case Keys.Escape
                    ClearSelection()
            End Select
        End Sub

        Private Function CreateBitmap(ByVal even As Boolean) As Bitmap
            If fView Is Nothing Then
                Return Nothing
            End If
            Dim result As New Bitmap(fView.GridControl.Bounds.Width, fView.GridControl.Bounds.Height)
            Using g As Graphics = Graphics.FromImage(result)
                Dim foreColor As Color = If(even, Color.AliceBlue, Color.AntiqueWhite)
                Dim backColor As Color = If(even, Color.AntiqueWhite, Color.AliceBlue)
                Using brush As Brush = New HatchBrush(HatchStyle.BackwardDiagonal, foreColor, backColor)
                    g.FillRectangle(brush, New Rectangle(Point.Empty, result.Size))
                End Using
            End Using
            result.Tag = "DXSample"
            Return result
        End Function

        Private Sub OnGridViewCustomDrawCell(ByVal sender As Object, ByVal e As RowCellCustomDrawEventArgs)
            If selectedArea.X = Integer.MaxValue Then
                Return
            End If
            Dim drawingArea As Rectangle = Rectangle.Empty
            If e.RowHandle = selectedArea.Top AndAlso IsPointInRange(e.Column.VisibleIndex, False) Then
                drawingArea = New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, selectionBorderWidth)
                If e.Column.VisibleIndex = selectedArea.Left Then
                    drawingArea.X += selectionBorderWidth
                    drawingArea.Width -= selectionBorderWidth
                End If
            End If
            If drawingArea <> Rectangle.Empty Then
                DrawArea(e.Cache, drawingArea)
                drawingArea = Rectangle.Empty
            End If
            If e.RowHandle = selectedArea.Bottom AndAlso IsPointInRange(e.Column.VisibleIndex, False) Then
                drawingArea = New Rectangle(e.Bounds.X, e.Bounds.Bottom - selectionBorderWidth, e.Bounds.Width, selectionBorderWidth)
                If e.Column.VisibleIndex = selectedArea.Right Then
                    drawingArea.Width -= selectionBorderWidth
                End If
            End If
            If drawingArea <> Rectangle.Empty Then
                DrawArea(e.Cache, drawingArea)
                drawingArea = Rectangle.Empty
            End If
            If e.Column.VisibleIndex = selectedArea.Left AndAlso IsPointInRange(e.RowHandle, True) Then
                drawingArea = New Rectangle(e.Bounds.X, e.Bounds.Y, selectionBorderWidth, e.Bounds.Height)
                If e.RowHandle = selectedArea.Bottom Then
                    drawingArea.Height -= selectionBorderWidth
                End If
            End If
            If drawingArea <> Rectangle.Empty Then
                DrawArea(e.Cache, drawingArea)
                drawingArea = Rectangle.Empty
            End If
            If e.Column.VisibleIndex = selectedArea.Right AndAlso IsPointInRange(e.RowHandle, True) Then
                drawingArea = New Rectangle(e.Bounds.Right - selectionBorderWidth, e.Bounds.Y, selectionBorderWidth, e.Bounds.Height)
                If e.RowHandle = selectedArea.Top Then
                    drawingArea.Y += selectionBorderWidth
                    drawingArea.Height -= selectionBorderWidth
                End If
            End If
            If drawingArea <> Rectangle.Empty Then
                DrawArea(e.Cache, drawingArea)
            End If
            If IsPointInRange(e.RowHandle, True) AndAlso IsPointInRange(e.Column.VisibleIndex, False) Then
                Dim info As GridCellInfo = TryCast(e.Cell, GridCellInfo)
                e.Appearance.DrawString(e.Cache, e.DisplayText, info.CellValueRect)
                e.Handled = True
            End If
        End Sub

        Private Sub DrawArea(ByVal cache As GraphicsCache, ByVal area As Rectangle)
            area.Inflate(1, 1)
            Using brush As Brush = New TextureBrush(If(even, EvenPicture, OddPicture))
                cache.FillRectangle(brush, area)
            End Using
        End Sub

        Private Function IsPointInRange(ByVal point As Integer, ByVal vertical As Boolean) As Boolean
            If vertical Then
                Return point >= selectedArea.Top AndAlso point <= selectedArea.Bottom
            Else
                Return point >= selectedArea.Left AndAlso point <= selectedArea.Right
            End If
        End Function

        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
            even = Not even
            If selectedArea.X = Integer.MaxValue Then
                Return
            End If
            For i As Integer = selectedArea.Left To selectedArea.Right
                For j As Integer = selectedArea.Top To selectedArea.Bottom
                    fView.InvalidateRowCell(j, fView.VisibleColumns(i))
                Next j
            Next i
        End Sub

        Private Sub ClearSelection()
            selectedArea.X = Integer.MaxValue
            fView.Invalidate()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not disposed AndAlso disposing Then
                If fView IsNot Nothing Then
                    UnsubscribeViewEvents()
                    fView = Nothing
                End If
                If evenPicture_Renamed IsNot Nothing AndAlso Object.ReferenceEquals(evenPicture_Renamed.Tag, "DXSample") Then
                    evenPicture_Renamed.Dispose()
                    evenPicture_Renamed = Nothing
                End If
                If evenPicture_Renamed IsNot Nothing AndAlso Object.ReferenceEquals(oddPicture_Renamed.Tag, "DXSample") Then
                    oddPicture_Renamed.Dispose()
                    oddPicture_Renamed = Nothing
                End If
                If timer IsNot Nothing Then
                    timer.Stop()
                    RemoveHandler timer.Tick, AddressOf OnTimerTick
                    timer.Dispose()
                    timer = Nothing
                End If
                disposed = True
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub SubscribeViewEvents()
            AddHandler fView.KeyDown, AddressOf OnGridViewKeyDown
            AddHandler fView.CustomDrawCell, AddressOf OnGridViewCustomDrawCell
        End Sub

        Private Sub UnsubscribeViewEvents()
            RemoveHandler fView.KeyDown, AddressOf OnGridViewKeyDown
            RemoveHandler fView.CustomDrawCell, AddressOf OnGridViewCustomDrawCell
        End Sub
    End Class
End Namespace