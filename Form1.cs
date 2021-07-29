using Syncfusion.WinForms.DataGrid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Interactivity;
using Syncfusion.WinForms.DataGrid.Enums;
using System.ComponentModel.DataAnnotations;

namespace SfDataGridDemo
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
            sfDataGrid.DataSource = new ViewModel().Orders;

            // set the customized CustomCellSelectionController to SfDataGrid.SelectionController when CellSelection applied in SfDataGrid
            //sfDataGrid.SelectionUnit = SelectionUnit.Cell;
            //sfDataGrid.SelectionController = new CustomCellSelectionController(this.sfDataGrid);

            // set the customized CustomSelectionController to SfDataGrid.SelectionController when RowSelection applied in SfDataGrid
            sfDataGrid.SelectionUnit = SelectionUnit.Row;
            sfDataGrid.SelectionController = new CustomSelectionController(this.sfDataGrid);
        }        
    }

    //Inherits the CustomCellSelectionController Class
    public class CustomCellSelectionController : CellSelectionController
    {
        public CustomCellSelectionController(SfDataGrid dataGrid)
            : base(dataGrid)
        {
        }

        //overriding the HandleKeyOperations Event from CellSelectionController base class
        protected override void HandleKeyOperations(KeyEventArgs args)
        {
            //Key based Customization 
            if (args.KeyCode == Keys.PageUp || args.KeyCode == Keys.PageDown)
            {
                //assigning the state of Tab key Event handling to PageUp and PageDown key
                KeyEventArgs arguments = new KeyEventArgs(Keys.Tab);
                base.HandleKeyOperations(arguments);
                return;
            }

            base.HandleKeyOperations(args);
        }
    }

    //Inherits the CustomSelectionController Class
    public class CustomSelectionController : RowSelectionController
    {
        public CustomSelectionController(SfDataGrid dataGrid)
            : base(dataGrid)
        {
        }

        //overriding the HandleKeyOperations Event from RowSelectionController base class
        protected override void HandleKeyOperations(KeyEventArgs args)
        {
            //Key based Customization 
            if (args.KeyCode == Keys.PageUp || args.KeyCode == Keys.PageDown)
            {              
                //assigning the state of Tab key Event handling to PageUp and PageDown key
                KeyEventArgs arguments = new KeyEventArgs(Keys.Tab);
                base.HandleKeyOperations(arguments);
                return;               
            }

            base.HandleKeyOperations(args);
        }
    }

    public class OrderInfo : INotifyPropertyChanged
    {
        decimal? orderID;
        string customerId;
        string country;
        string customerName;
        string shippingCity;
        bool isShipped;

        public OrderInfo()
        {

        }

        [Display(Name = "Order ID")]

        public decimal? OrderID
        {
            get { return orderID; }
            set { orderID = value; this.OnPropertyChanged("OrderID"); }
        }

        [Display(Name = "Customer ID")]
        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; this.OnPropertyChanged("CustomerID"); }
        }

        [Display(Name = "Customer Name")]
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; this.OnPropertyChanged("CustomerName"); }
        }

        [Display(Name = "Country")]
        public string Country
        {
            get { return country; }
            set { country = value; this.OnPropertyChanged("Country"); }
        }

        [Display(Name = "Ship City")]
        public string ShipCity
        {
            get { return shippingCity; }
            set { shippingCity = value; this.OnPropertyChanged("ShipCity"); }
        }

        [Display(Name = "Is Shipped")]
        public bool IsShipped
        {
            get { return isShipped; }
            set { isShipped = value; this.OnPropertyChanged("IsShipped"); }
        }


        public OrderInfo(decimal? orderId, string customerName, string country, string customerId, string shipCity, bool isShipped)
        {
            this.OrderID = orderId;
            this.CustomerName = customerName;
            this.Country = country;
            this.CustomerID = customerId;
            this.ShipCity = shipCity;
            this.IsShipped = isShipped;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel
    {
        private ObservableCollection<OrderInfo> orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public ViewModel()
        {
            orders = new ObservableCollection<OrderInfo>();
            orders.Add(new OrderInfo(1001, "Thomas Hardy", "Germany", "ALFKI", "Berlin", true));
            orders.Add(new OrderInfo(1002, "Laurence Lebihan", "Mexico", "ANATR", "Mexico", false));
            orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico", true));
            orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London", true));
            orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
        }
    }
}
