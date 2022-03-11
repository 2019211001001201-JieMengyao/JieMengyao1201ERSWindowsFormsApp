using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace JieMengyao1201ERSWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            initalizeListView();
        }

        private void statusBar1_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {

        }

        private void EmployeeRecordsForm_Load(object sender, EventArgs e)
        {

        }
        private void PopulateTreeView()
        {
            statusBarPanel1.Tag = "Refreshing Employee Code. Please Wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("D:\\c#\\CASE_2021-22.2\\MyRepos\\JieMengyao1201ERSWindowsFormsApp\\JieMengyao1201ERSWindowsFormsApp\\EmpRec.xml");
            reader.MoveToContent();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToContent();
                        reader.MoveToContent();

                        reader.MoveToAttribute("Id");
                        String strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();
                        }
                        TreeNode EcodeNode = new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);

                    }
                }
                statusBarPanel1.Tag = "Click on an employee code to see their record.";
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void initalizeListView()
        {
            listView1.Clear();
            listView1.Columns.Add("Employee Name", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join", 70, HorizontalAlignment.Right);
            listView1.Columns.Add("Gread", 105, HorizontalAlignment.Left);
            listView1.Columns.Add("Salary", 105, HorizontalAlignment.Left);

        }
        protected void PopulateListView(TreeNode crrNode)
        {
            initalizeListView();
            XmlTextReader listRead = new XmlTextReader("D:\\c#\\CASE_2021-22.2\\MyRepos\\JieMengyao1201ERSWindowsFormsApp\\JieMengyao1201ERSWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new string[4];
                listRead.MoveToFirstAttribute();//Id="E001"
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if (strNodeName == strNodePath)
                {
                    ListViewItem lvi;
                    listRead.MoveToNextAttribute();
                    name = listRead.Value;//name "Michael Preey"
                    lvi = listView1.Items.Add(name);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj = listRead.Value;//DateofJoin="02-02-1999
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute();
                    gread = listRead.Value;//Gread="A"
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute();
                    sal = listRead.Value;//Salary="1750"
                    lvi.SubItems.Add(sal);

                    listRead.MoveToNextAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();
                }

            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                statusBarPanel1.Text = "Double Click the Employ Records.";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an Emplyoee code to view Individual record";
            }
            PopulateListView(currNode);

        }
    }
}
