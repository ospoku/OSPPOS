namespace OSPPOS.Models
{
    public class StockTransactionType
    {
      
            public const string Purchase = "PURCHASE";   // StockBatch
            public const string Sale = "SALE";           // Invoice
            public const string ReturnIn = "RETURN_IN";  // Customer return
            public const string ReturnOut = "RETURN_OUT";// Supplier return
            public const string Adjustment = "ADJUST";   // Manual fix
        }
    }

