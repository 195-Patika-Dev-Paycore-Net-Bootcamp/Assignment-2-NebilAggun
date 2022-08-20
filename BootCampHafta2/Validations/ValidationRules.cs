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
            RuleFor(x => x.Email).Matches(new Regex(@"^[a-zA-Z\.@]{2,100}$")).
               WithMessage("Email içerisinde özel karakter olmamalı.");

            //telefon numarasının + ile başlama validasyonu
            RuleFor(x => x.PhoneNumber).Must(PhoneNumber => 
            PhoneNumber.StartsWith("+")).
                WithMessage("+90 ile başlayarak giriniz.");
        }
    }
}
