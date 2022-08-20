using BootCampHafta2.Entity;
using BootCampHafta2.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BootCampHafta2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        //ekleme silme güncelleme işlemlerinin yapılacağı liste.
        private static List<Personal> _liste = new List<Personal>();

        public PersonalController()
        {

        }

        //Tüm listeyi görüntülemek için oluşturulmuş Get metodu geriye listeyi döndürür.
        [HttpGet("ListPersonal")]
        public List<Personal> GetAllPersona()
        {
            return _liste;
        }

        //Verilen Id'ye göre listede aynı ıd'ye ait olan ilk personeli alır ve geriye döndürür.
        [HttpGet("GetById/{id}")]
        public Personal GetById([FromRoute] long id)
        {
            var personal = _liste.Where(x => x.Id == id).FirstOrDefault();
            return personal;
        }
        //Post metodu ile personelden bir personel metodu oluşturularak değişkenlerin girilmesi istenir.
        //ardından validasyon sınıfınfan türetilen metotla paarametrelerin kurallara uygunluğu kontrol edilir.
        //eğer uygunsa personel listeye eklenir ve geriye döndürülür eğer değilse geriyehatalar döner.

        [HttpPost]
        public IActionResult AddPersonal([FromQuery] Personal personal)
        {
            ValidationRules validationRules = new ValidationRules();
            ValidationResult result = validationRules.Validate(personal);
            if (result.IsValid)
            {
                _liste.Add(personal);
                return Ok(personal);
            }
            else
            {
                foreach (var item in result.Errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                return BadRequest(ModelState);
            }
        }

        //Put metodu ile öncelikle güncellencek personel Id'si parametre olarak alınır.
        //personelden bir personel metodu oluşturularak değişkenlerin girilmesi istenir.
        //ardından validasyon sınıfınfan türetilen metotla paarametrelerin kurallara uygunluğu kontrol edilir.
        //eğer uygunsa personel listedeki personel güncellenir ve geriye döndürülür eğer değilse geriyehatalar döner.
        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Personal updatePersonal)
        {
            var personal = _liste.SingleOrDefault(x => x.Id == id);
            ValidationRules validationRules = new ValidationRules();
            ValidationResult result = validationRules.Validate(updatePersonal);

            if (personal is null) return BadRequest();
            else
            {

                if (result.IsValid)
                {
                    personal.Id= personal.Id;
                    personal.Name = updatePersonal.Name;
                    personal.Lastname = updatePersonal.Lastname;
                    personal.PhoneNumber = updatePersonal.PhoneNumber;
                    personal.Salary = updatePersonal.Salary;
                    personal.DateOfBirth = updatePersonal.DateOfBirth;
                    personal.Email = updatePersonal.Email;
                    return Ok(personal);
                }
                else
                {
                    foreach (var item in result.Errors)
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                    return BadRequest(ModelState);
                }
            }
        }

        //girilen Id değerine göre o personel bulunulur. eğer öyle bir personel yoksa geriye
        //badrequest dönülür varsa personel listeden silinir ve 201 dönülür.
        [HttpDelete]
        public ActionResult DeletePersonal([FromRoute] int id)
        {
            var personal = _liste.SingleOrDefault(x => x.Id == id);
            if (personal is null)
            {
                return BadRequest();
            }
            else
            {
                _liste.Remove(personal);
                return Ok(201);
            }
        }
    }
}
