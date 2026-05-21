using BunlyWebApi.Entities;
using FluentValidation;

namespace BunlyWebApi.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş geçilemez.")
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Ürün adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("Ürün açıklaması boş geçilemez.")
                .MinimumLength(10).WithMessage("Ürün açıklaması en az 10 karakter olmalıdır.")
                .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olabilir.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Ürün görseli boş geçilemez.")
                .Must(BeAValidUrl).WithMessage("Geçerli bir görsel URL adresi giriniz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir kategori seçiniz.");
        }

        private bool BeAValidUrl(string imageUrl)
        {
            return Uri.TryCreate(imageUrl, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
