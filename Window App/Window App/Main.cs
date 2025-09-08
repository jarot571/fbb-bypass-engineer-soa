using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Window_App.Common;
using Window_App.WF;

namespace Window_App
{
    public partial class Main : Form
    {
        public Main()
        {
            //this.Icon = new Icon(@"logo.ico"); // path to your icon
            InitializeComponent();
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetCheckedRows();
        }

        private bool checkboxAdded = false;
        private JArray originalJsonArray;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string jsonContent = File.ReadAllText(ofd.FileName);
                    if (string.IsNullOrWhiteSpace(jsonContent)) return;

                    try
                    {
                        // Parse original JSON
                        if (jsonContent.TrimStart().StartsWith("["))
                        {
                            originalJsonArray = JArray.Parse(jsonContent);
                        }
                        else
                        {
                            var obj = JObject.Parse(jsonContent);
                            originalJsonArray = new JArray(obj);
                        }

                        // Filter rows with accessMode not null
                        var filtered = new List<Dictionary<string, object>>();
                        foreach (JObject row in originalJsonArray)
                        {
                            if (row["accessMode"] != null && !string.IsNullOrWhiteSpace(row["accessMode"].ToString()))
                            {
                                filtered.Add(row.ToObject<Dictionary<string, object>>());
                            }
                        }

                        if (filtered.Count == 0)
                        {
                            MessageBox.Show("No valid records found (all had null accessMode).");
                            return;
                        }

                        // Load into DataGridView
                        var dt = new DataTable();
                        foreach (var key in filtered[0].Keys)
                        {
                            dt.Columns.Add(key);
                        }
                        foreach (var row in filtered)
                        {
                            var dr = dt.NewRow();
                            foreach (var key in row.Keys)
                                dr[key] = row[key]?.ToString() ?? "";
                            dt.Rows.Add(dr);
                        }

                        GridViewDisplay.DataSource = dt;

                        if (!checkboxAdded)
                        {
                            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                            chk.Name = "Select";
                            chk.HeaderText = "✔";
                            chk.Width = 50;
                            GridViewDisplay.Columns.Insert(0, chk);
                            checkboxAdded = true;
                        }
                        GridViewDisplay.AllowUserToAddRows = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error parsing JSON: " + ex.Message);
                    }
                }
            }
        }

        private List<Dictionary<string, object>> GetCheckedRows()
        {
            var selectedRows = new List<Dictionary<string, object>>();

            // Ensure any pending edit is committed
            GridViewDisplay.EndEdit();

            foreach (DataGridViewRow row in GridViewDisplay.Rows)
            {
                if (row.Cells["Select"] is DataGridViewCheckBoxCell chkCell &&
                    chkCell.Value != null &&
                    (bool)chkCell.Value)
                {
                    var record = new Dictionary<string, object>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn.Name != "Select")
                        {
                            record[cell.OwningColumn.Name] = cell.Value;
                        }
                    }
                    selectedRows.Add(record);
                }
            }

            return selectedRows;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process();
        }

        private async void Process()
        {
            if (originalJsonArray == null)
            {
                MessageBox.Show("No JSON loaded.");
                return;
            }

            var selectedIndexes = new List<int>();
            GridViewDisplay.EndEdit();

            for (int i = 0; i < GridViewDisplay.Rows.Count; i++)
            {
                var row = GridViewDisplay.Rows[i];
                if (row.Cells["Select"] is DataGridViewCheckBoxCell chkCell &&
                    chkCell.Value != null &&
                    (bool)chkCell.Value)
                {
                    selectedIndexes.Add(i);
                }
            }

            if (selectedIndexes.Count == 0)
            {
                MessageBox.Show("No rows selected.");
                return;
            }

            GetConfig conf = new GetConfig();
            string bearer = conf.GetConfigText("access_token");
            string apiUrl = conf.GetConfigText("mychannel");

            Core api = new Core();
            // Assuming this now returns List<JObject> or List<JArray> for each response
            var responses = await api.PostSelectedOriginalJsonAsync(originalJsonArray, selectedIndexes, bearer, apiUrl);

            if (responses.Count > 0)
            {
                GetSaleOrderIds(responses);
                var InternetNo = await api.PostSoapRequestAsync(apiUrl, bearer, bearer, GetSaleOrderIds(responses));
            }
            else
            {
                MessageBox.Show("Error: Please contact Kapom.");
            }
        }

        private string GetSaleOrderIds(List<string> responses)
        {
            string saleOrderId = "";
            if (responses == null || responses.Count == 0)
            {
                MessageBox.Show("No responses to display.");
                return "";
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("saleOrderId");

            foreach (string res in responses)
            {
                var obj = JObject.Parse(res);

                var data = obj["data"] as JObject;
                if (data != null)
                {
                    // Try both variations
                    saleOrderId =
                        data["saleOrderId"]?.ToString() ??
                        data["saleOrderID"]?.ToString();

                    if (!string.IsNullOrEmpty(saleOrderId))
                    {
                        var row = dt.NewRow();
                        row["saleOrderId"] = saleOrderId;
                        dt.Rows.Add(row);
                    }
                }
            }

            GridViewDisplay.DataSource = dt;
            if (!GridViewDisplay.Columns.Contains("Select"))
            {
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "✔",
                    Width = 50
                };
                GridViewDisplay.Columns.Insert(0, chk);
            }

            GridViewDisplay.AllowUserToAddRows = false;

            return saleOrderId;
        }


        //MessageBox.Show($"Values entered:\n{airnet}\n{airnet}\n{fbss}\n{access_token}");
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 700,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Create Configuation Enpoint",
                StartPosition = FormStartPosition.CenterScreen,
            };

            // Custom labels
            string[] labelNames = new string[] { "MyChannel :", "Airnet :", "FBSS :", "Token :" };
            string[] defaultValues = new string[]
            {
                "https://sit-mychannel.cdc.ais.th/api/fibreportalais/orders-v2",
                "https://staging-airnetregws.ais.co.th/airnet-service/AIRInterfaceIMWebService",
                "https://10.138.25.20:7606/axis2/services/OrderService",
                "<bearer from mychannel>"
            };

            Label[] labels = new Label[4];
            TextBox[] textboxes = new TextBox[4];

            for (int i = 0; i < 4; i++)
            {
                labels[i] = new Label() 
                { 
                    Left = 10, 
                    Top = 20 + i * 30, 
                    Text = labelNames[i], 
                    Width = 110 
                };
                textboxes[i] = new TextBox() 
                { 
                    Left = 120, 
                    Top = 20 + i * 30, 
                    Width = 530, 
                    Text = defaultValues[i],
                    BorderStyle = BorderStyle.FixedSingle,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right 
                }; 
                prompt.Controls.Add(labels[i]);
                prompt.Controls.Add(textboxes[i]);
            }

            Button okBtn = new Button() { Text = "OK", Left = 120, Width = 80, Height = 30, Top = 150, DialogResult = DialogResult.OK };
            Button cancelBtn = new Button() { Text = "Cancel", Left = 230, Width = 80, Height = 30, Top = 150, DialogResult = DialogResult.Cancel };
            prompt.Controls.Add(okBtn);
            prompt.Controls.Add(cancelBtn);

            prompt.AcceptButton = okBtn;
            prompt.CancelButton = cancelBtn;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                // Collect values
                string mychannel = textboxes[0].Text;
                string airnet = textboxes[1].Text;
                string fbss = textboxes[2].Text;
                string access_token = textboxes[3].Text;

                // Create lines in format name="value"
                var lines = new List<string>()
                {
                    $"mychannel={mychannel}",
                    $"airnet={airnet}",
                    $"fbss={fbss}",
                    $"access_token={access_token}"
                };

                // Specify folder
                string folderPath = @"D:\Config";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // create folder if it doesn't exist
                }

                string filePath = Path.Combine(folderPath, "config.txt");
                File.WriteAllLines(filePath, lines);

                MessageBox.Show($" Saved to: {filePath}");
            }
        }

        private void deviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalJsonArray == null || originalJsonArray.Count == 0)
            {
                MessageBox.Show("No JSON loaded to save.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                sfd.Title = "Save Json As";
                sfd.FileName = "template.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = @"D:\Config"; // your specific folder
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string filePath = Path.Combine(folderPath, "template.json"); // specific filename

                    try
                    {
                        File.WriteAllText(filePath, originalJsonArray.ToString(Newtonsoft.Json.Formatting.Indented));
                        MessageBox.Show($"file saved to:\n{filePath}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving JSON: " + ex.Message);
                    }
                }
            }
        }
    }
}
