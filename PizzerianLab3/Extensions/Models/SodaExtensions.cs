using System;
using System.Collections.Generic;
using System.Linq;
using PizzerianLab3.Data.Entities;
using PizzerianLab3.Models.Display;

namespace PizzerianLab3.Extensions.Models
{
    public static class SodaExtensions
    {
        public static SodaViewModel ToViewModel(this Soda soda)
        {
            return new()
            {
                MenuNumber = soda.MenuNumber,
                Name = soda.Name,
                Price = soda.Price
            };
        }

        public static IEnumerable<SodaViewModel> ToViewModels(this IEnumerable<Soda> sodas)
        {
            return sodas.Select(x => x.ToViewModel());
        }

        public static Soda CopyToNew(this Soda soda)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = soda.Name,
                Price = soda.Price,
                MenuNumber = soda.MenuNumber
            };
        }
    }
}