using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Shared
{
    public class SuccessMessages
    {
        
        public static string Register = "ثبت نام با موفقیت انجام گردید";
        public static string CreateData = "اطلاعات  با موفقیت ثبت گردید";
        public static string UpdateData = "اطلاعات  با موفقیت بروزرسانی گردید";
        public static string DeleteData = "اطلاعات  با موفقیت حذف گردید";
        public static string ChangeData = "تغییرات با موفقیت انجام شد";
        public static string SendCode = "کد یکبار مصرف ارسال گردید";
        public static string AddToOrder = "محصول به سبد خریداضافه گردید";
        public static string AddComment = "نظر شما ثبت و بعداز تایید نمایش داده می شود";
        public static string ChargeWallet = "کیف پول با موفقیت شارژ شد";
        public static string ContactEmail = "پاسخ با موفقیت ارسال گردید";

    }

    public class InfoMessages
    {
        public static string Welcome = "خـــــوش آمــــدید";

    }
    public class ErrorMessages
    {
        public static string OprationFaild = "خطایی رخ داده است لطفاً بعداً تلاش نمایید";

    }
    public class WarningMessages
    {
        public static string InserField = "کلیه اطلاعات اجباری را وارد نمایید";
        public static string NotFoundData = "اطلاعاتی یافت نشد";
        public static string DublicatedData = "اطلاعات وارد شده قبلاً ثبت گردیده است";
        public static string DublicatedMobile = "موبایل وارد شده قبلاً ثبت گردیده است";
        public static string DublicatedEmail = "ایمیل وارد شده قبلاً ثبت گردیده است";
        public static string DublicatedUserName= "نام کاربری وارد شده قبلاً ثبت گردیده است";
        public static string DublicatedSlug= "عنوان مرورگر وارد شده قبلاً ثبت گردیده است";
        public static string InvalidDelete = "مجاز به حذف اطلاعات  نمی باشید";
        public static string NotActiveUser = "حساب کاربری شما غیر فعال است";
        public static string QuantityPlus = "تعداددرخواست بیش از موجودی می باشد";
        public static string LessAmount = "مبلغ وارد شده کمتر از حد مجاز می باشد";
        public static string ImageInvalid = "تصویری اسلایدر را انتخاب  نمایید";
        public static string MoreThanAllowed = " مجاز به افزودن اسلایدر جدید نمی باشد";


    }
}
