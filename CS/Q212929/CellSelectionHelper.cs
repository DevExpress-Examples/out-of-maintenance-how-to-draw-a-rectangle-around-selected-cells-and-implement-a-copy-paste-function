using DevExpress.XtraGrid.Views.Grid;
using System;
using DevExpress.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Drawing;

namespace DXSample {
    public class CellSelectionHelper :Component {
        private bool disposed = false;
        private Rectangle selectedArea = Rectangle.Empty;
        private Bitmap evenPicture;
        private Bitmap oddPicture;
        private bool even = true;
        private Timer timer;

        private const int selectionBorderWidth = 3;

        public CellSelectionHelper() {
            if (!DesignMode) {
                timer = new Timer();
                timer.Tick += new EventHandler(OnTimerTick);
                timer.Start();
            }
            selectedArea.X = int.MaxValue;
        }

        private GridView fView;
        public GridView View {
            get { return fView; }
            set {
                if (fView == value) return;
                if (fView != null)
                    UnsubscribeViewEvents();
                fView = value;
                if (value != null) {
                    SubscribeViewEvents();
                    fView.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
                }
            }
        }

        public Bitmap EvenPicture {
            get {
                if (evenPicture == null)
                    evenPicture = CreateBitmap(true);
                return evenPicture;
            }
            set {
                if (evenPicture != null && evenPicture.Tag.ToString() == "DXSample")
                    evenPicture.Dispose();
                evenPicture = value;
            }
        }

        public Bitmap OddPicture {
            get {
                if (oddPicture == null)
                    oddPicture = CreateBitmap(false);
                return oddPicture;
            }
            set {
                if (oddPicture != null && oddPicture.Tag.ToString() == "DXSample")
                    oddPicture.Dispose();
                oddPicture = value;
            }
        }

        private void OnGridViewKeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.C:
                    if (e.Control) {
                        ClearSelection();
                        GridCell[] cells = fView.GetSelectedCells();
                        if (cells.Length == 0) return;
                        int minCol, minRow, maxCol, maxRow;
                        minCol = minRow = int.MaxValue;
                        maxCol = maxRow = 0;
                        foreach (GridCell cell in cells) {
                            minCol = Math.Min(cell.Column.VisibleIndex, minCol);
                            minRow = Math.Min(cell.RowHandle, minRow);
                            maxCol = Math.Max(cell.Column.VisibleIndex, maxCol);
                            maxRow = Math.Max(cell.RowHandle, maxRow);
                        }
                        if ((maxCol - minCol + 1) * (maxRow - minRow + 1) != cells.Length) {
                            XtraMessageBox.Show("This action won't work on multiple selections.");
                            e.SuppressKeyPress = true;
                            break;
                        }
                        selectedArea = new Rectangle(minCol, minRow, maxCol - minCol, maxRow - minRow);
                    }
                    break;
                case Keys.Escape: ClearSelection(); break;
            }
        }

        private Bitmap CreateBitmap(bool even) {
            if (fView == null) return null;
            Bitmap result = new Bitmap(fView.GridControl.Bounds.Width, fView.GridControl.Bounds.Height);
            using (Graphics g = Graphics.FromImage(result)) {
                Color foreColor = even ? Color.AliceBlue : Color.AntiqueWhite;
                Color backColor = even ? Color.AntiqueWhite : Color.AliceBlue;
                using (Brush brush = new HatchBrush(HatchStyle.BackwardDiagonal, foreColor, backColor)) {
                    g.FillRectangle(brush, new Rectangle(Point.Empty, result.Size));
                }
            }
            result.Tag = "DXSample";
            return result;
        }

        private void OnGridViewCustomDrawCell(object sender, RowCellCustomDrawEventArgs e) {
            if (selectedArea.X == int.MaxValue) return;
            Rectangle drawingArea = Rectangle.Empty;
            if (e.RowHandle == selectedArea.Top && IsPointInRange(e.Column.VisibleIndex, false)) {
                drawingArea = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, selectionBorderWidth);
                if (e.Column.VisibleIndex == selectedArea.Left) {
                    drawingArea.X += selectionBorderWidth;
                    drawingArea.Width -= selectionBorderWidth;
                }
            }
            if (drawingArea != Rectangle.Empty) {
                DrawArea(e.Cache, drawingArea);
                drawingArea = Rectangle.Empty;
            }
            if (e.RowHandle == selectedArea.Bottom && IsPointInRange(e.Column.VisibleIndex, false)) {
                drawingArea = new Rectangle(e.Bounds.X, e.Bounds.Bottom - selectionBorderWidth, e.Bounds.Width, selectionBorderWidth);
                if (e.Column.VisibleIndex == selectedArea.Right) drawingArea.Width -= selectionBorderWidth;
            }
            if (drawingArea != Rectangle.Empty) {
                DrawArea(e.Cache, drawingArea);
                drawingArea = Rectangle.Empty;
            }
            if (e.Column.VisibleIndex == selectedArea.Left && IsPointInRange(e.RowHandle, true)) {
                drawingArea = new Rectangle(e.Bounds.X, e.Bounds.Y, selectionBorderWidth, e.Bounds.Height);
                if (e.RowHandle == selectedArea.Bottom) drawingArea.Height -= selectionBorderWidth;
            }
            if (drawingArea != Rectangle.Empty) {
                DrawArea(e.Cache, drawingArea);
                drawingArea = Rectangle.Empty;
            }
            if (e.Column.VisibleIndex == selectedArea.Right && IsPointInRange(e.RowHandle, true)) {
                drawingArea = new Rectangle(e.Bounds.Right - selectionBorderWidth, e.Bounds.Y, selectionBorderWidth,
                    e.Bounds.Height);
                if (e.RowHandle == selectedArea.Top) {
                    drawingArea.Y += selectionBorderWidth;
                    drawingArea.Height -= selectionBorderWidth;
                }
            }
            if (drawingArea != Rectangle.Empty) DrawArea(e.Cache, drawingArea);
            if (IsPointInRange(e.RowHandle, true) && IsPointInRange(e.Column.VisibleIndex, false))
            {
                GridCellInfo info = e.Cell as GridCellInfo;
                e.Appearance.DrawString(e.Cache, e.DisplayText, info.CellValueRect);
                e.Handled = true;
            }
        }

        private void DrawArea(GraphicsCache cache, Rectangle area) {
            area.Inflate(1, 1);
            using (Brush brush = new TextureBrush(even ? EvenPicture : OddPicture)) {
                cache.FillRectangle(brush, area);
            }
        }

        private bool IsPointInRange(int point, bool vertical) {
            if (vertical) return point >= selectedArea.Top && point <= selectedArea.Bottom;
            else return point >= selectedArea.Left && point <= selectedArea.Right;
        }

        private void OnTimerTick(object sender, EventArgs e) { 
            even = !even;
            if (selectedArea.X == int.MaxValue) return;
            for (int i = selectedArea.Left; i <= selectedArea.Right; i++)
                for (int j = selectedArea.Top; j <= selectedArea.Bottom; j++)
                    fView.InvalidateRowCell(j, fView.VisibleColumns[i]);
        }

        private void ClearSelection() {
            selectedArea.X = int.MaxValue;
            fView.Invalidate();
        }

        protected override void Dispose(bool disposing) {
            if (!disposed && disposing) {
                if (fView != null) {
                    UnsubscribeViewEvents();
                    fView = null;
                }
                if (evenPicture != null && object.ReferenceEquals(evenPicture.Tag, "DXSample")) {
                    evenPicture.Dispose();
                    evenPicture = null;
                }
                if (evenPicture != null && object.ReferenceEquals(oddPicture.Tag, "DXSample")) {
                    oddPicture.Dispose();
                    oddPicture = null;
                }
                if (timer != null) {
                    timer.Stop();
                    timer.Tick -= new EventHandler(OnTimerTick);
                    timer.Dispose();
                    timer = null;
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }

        private void SubscribeViewEvents() {
            fView.KeyDown += new KeyEventHandler(OnGridViewKeyDown);
            fView.CustomDrawCell += new RowCellCustomDrawEventHandler(OnGridViewCustomDrawCell);
        }

        private void UnsubscribeViewEvents() {
            fView.KeyDown -= new KeyEventHandler(OnGridViewKeyDown);
            fView.CustomDrawCell -= new RowCellCustomDrawEventHandler(OnGridViewCustomDrawCell);
        }
    }
}