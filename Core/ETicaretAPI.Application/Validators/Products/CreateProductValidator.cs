using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<CreateProductVM>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen isim giriniz")
                .MaximumLength(150)
                .MinimumLength(5).WithMessage("İsim minumum 5 maksimum 150 karakter olmalı");
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen stok bilgisi giriniz")
                .Must(s => s >= 0).WithMessage("Stok bilgisi negatif olamaz!");
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull().WithMessage("Lütfen fiyat bilgisi giriniz")
                .Must(p => p >= 0).WithMessage("Fiyat negatif olamaz!");
        }
    }
}
