

namespace OnlineStore.Application.Extensions
{
    public static class MoneyExtition
    {
        public static string ToMoney(this int money)
        {
            return money.ToString("#,0");
        }
    }
}
