using BootCampHafta2.Entity;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace BootCampHafta2.Validations
{
    public class ValidationRules : AbstractValidator<Personal>
    {
        public ValidationRules()
        {
            //doğum tarihi kontrolü ve geriye dönen mesaj.
            RuleFor(x => x.DateOfBirth).Must(DateOfBirth =>
            DateOfBirth < new DateTime(2002, 10, 10, 00, 00, 00)
            && DateOfBirth > new DateTime(1945, 11, 11, 00, 00, 00)).
                WithMessage("1945-11-111 ile 2002-1010 tarhileri arasını giriniz.");

            //email içerisinde özel karakter olmamamsı için gerekli validasyon ve dönen mesaj.
            RuleFor(x => x.Email).Custom((email, context) => 
            {
                var arr = new[] {'0','1','2','3','4','5','6','7','8','9','-','|','[',']',
                '_','!','*','^','%','&','/','=','?','(',')','>','<','#','$','½','{','}'};
                for (int i = 0; i < email.Length; i++)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (email[i] == arr[j])
                        {
                            context.AddFailure("Email içerisinde . harici özel karakter bulunamaz");
                        }
                    }
                }
            });
            //telefon numarasının + ile başlama validasyonu
            RuleFor(x => x.PhoneNumber).Must(PhoneNumber => 
            PhoneNumber.StartsWith("+")).
                WithMessage("+90 ile başlayarak giriniz.");
        }
    }
}
