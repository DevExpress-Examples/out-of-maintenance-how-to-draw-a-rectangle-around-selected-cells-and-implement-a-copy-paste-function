Namespace Q212929
    Partial Public Class Form1
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(Form1))
            Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
            Me.bindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
            Me.dataSet1 = New System.Data.DataSet()
            Me.dataTable1 = New System.Data.DataTable()
            Me.dataColumn1 = New System.Data.DataColumn()
            Me.dataColumn2 = New System.Data.DataColumn()
            Me.dataColumn3 = New System.Data.DataColumn()
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.colColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.cellSelectionHelper1 = New DXSample.CellSelectionHelper()
            CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' gridControl1
            ' 
            Me.gridControl1.DataSource = Me.bindingSource1
            Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridControl1.Location = New System.Drawing.Point(0, 0)
            Me.gridControl1.MainView = Me.gridView1
            Me.gridControl1.Name = "gridControl1"
            Me.gridControl1.Size = New System.Drawing.Size(400, 273)
            Me.gridControl1.TabIndex = 0
            Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
            ' 
            ' bindingSource1
            ' 
            Me.bindingSource1.DataMember = "Table1"
            Me.bindingSource1.DataSource = Me.dataSet1
            ' 
            ' dataSet1
            ' 
            Me.dataSet1.DataSetName = "NewDataSet"
            Me.dataSet1.Tables.AddRange(New System.Data.DataTable() { Me.dataTable1})
            ' 
            ' dataTable1
            ' 
            Me.dataTable1.Columns.AddRange(New System.Data.DataColumn() { Me.dataColumn1, Me.dataColumn2, Me.dataColumn3})
            Me.dataTable1.TableName = "Table1"
            ' 
            ' dataColumn1
            ' 
            Me.dataColumn1.ColumnName = "Column1"
            ' 
            ' dataColumn2
            ' 
            Me.dataColumn2.ColumnName = "Column2"
            ' 
            ' dataColumn3
            ' 
            Me.dataColumn3.ColumnName = "Column3"
            ' 
            ' gridView1
            ' 
            Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colColumn1, Me.colColumn2, Me.colColumn3})
            Me.gridView1.GridControl = Me.gridControl1
            Me.gridView1.Name = "gridView1"
            Me.gridView1.OptionsSelection.MultiSelect = True
            Me.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            ' 
            ' colColumn1
            ' 
            Me.colColumn1.Caption = "A"
            Me.colColumn1.FieldName = "Column1"
            Me.colColumn1.Name = "colColumn1"
            Me.colColumn1.Visible = True
            Me.colColumn1.VisibleIndex = 0
            ' 
            ' colColumn2
            ' 
            Me.colColumn2.Caption = "B"
            Me.colColumn2.FieldName = "Column2"
            Me.colColumn2.Name = "colColumn2"
            Me.colColumn2.Visible = True
            Me.colColumn2.VisibleIndex = 1
            ' 
            ' colColumn3
            ' 
            Me.colColumn3.Caption = "C"
            Me.colColumn3.FieldName = "Column3"
            Me.colColumn3.Name = "colColumn3"
            Me.colColumn3.Visible = True
            Me.colColumn3.VisibleIndex = 2
            ' 
            ' cellSelectionHelper1
            ' 
            Me.cellSelectionHelper1.EvenPicture = (CType(resources.GetObject("cellSelectionHelper1.EvenPicture"), System.Drawing.Bitmap))
            Me.cellSelectionHelper1.OddPicture = (CType(resources.GetObject("cellSelectionHelper1.OddPicture"), System.Drawing.Bitmap))
            Me.cellSelectionHelper1.View = Me.gridView1
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(400, 273)
            Me.Controls.Add(Me.gridControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.bindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dataSet1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dataTable1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private gridControl1 As DevExpress.XtraGrid.GridControl
        Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
        Private bindingSource1 As System.Windows.Forms.BindingSource
        Private dataSet1 As System.Data.DataSet
        Private dataTable1 As System.Data.DataTable
        Private dataColumn1 As System.Data.DataColumn
        Private dataColumn2 As System.Data.DataColumn
        Private dataColumn3 As System.Data.DataColumn
        Private colColumn1 As DevExpress.XtraGrid.Columns.GridColumn
        Private colColumn2 As DevExpress.XtraGrid.Columns.GridColumn
        Private colColumn3 As DevExpress.XtraGrid.Columns.GridColumn
        Private cellSelectionHelper1 As DXSample.CellSelectionHelper
    End Class
End Namespace

