using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace Phoneword_iOS
{
    public partial class CallHistoryController : UITableViewController
    {
        public List<string> PhoneNumbers { get; set; }

        static NSString callHistoryCellId = new NSString("CallHistoryCell");

        public CallHistoryController(IntPtr handle)
            : base(handle)
        {
            TableView.RegisterClassForCellReuse(typeof(UITableViewCell), callHistoryCellId);
            TableView.Source = new CallHistoryDataSource(this);
            PhoneNumbers = new List<string>();
        }

        class CallHistoryDataSource : UITableViewSource
        {
            CallHistoryController controller;

            public CallHistoryDataSource(CallHistoryController controller)
            {
                this.controller = controller;
            }

            // テーブルの各セクションの行数を返します
            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return controller.PhoneNumbers.Count;
            }

            // NSIndexPathのRowプロパティで指定された行のテーブルセルを返します
            // このメソッドは、表の各行を挿入するために複数回呼び出されます
            // このメソッドは自動的に画面外にスクロールしたCellを使用または必要に応じて新しいものを作成します
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CallHistoryController.callHistoryCellId);
                int row = indexPath.Row;
                cell.TextLabel.Text = controller.PhoneNumbers[row];
                return cell;
            }
        }
    }
}
