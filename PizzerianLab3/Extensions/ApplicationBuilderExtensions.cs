using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PizzerianLab3.Data;
using PizzerianLab3.Data.Entities;

namespace PizzerianLab3.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder @this)
        {
            using (var serviceScope =
                @this.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    context.Database.EnsureCreated();
                    if (!context.Menus.Any())
                    {
                        var pizzaMenu = new List<Pizza>
                        {
                            new()
                            {
                                MenuNumber = 1, Name = "Margerita", PizzaIngredients =
                                {
                                    new Ingredient {IngredientOption = IngredientEnum.Cheese},
                                    new Ingredient {IngredientOption = IngredientEnum.Tomatosauce}
                                },
                                BasePrice = 85
                            },

                            new()
                            {
                                MenuNumber = 2, Name = "Hawaii", PizzaIngredients =
                                {
                                    new Ingredient {IngredientOption = IngredientEnum.Cheese},
                                    new Ingredient {IngredientOption = IngredientEnum.Tomatosauce},
                                    new Ingredient {IngredientOption = IngredientEnum.Ham},
                                    new Ingredient {IngredientOption = IngredientEnum.Pineapple}
                                },
                                BasePrice = 95
                            },

                            new()
                            {
                                MenuNumber = 3, Name = "Kebabpizza", PizzaIngredients =
                                {
                                    new Ingredient {IngredientOption = IngredientEnum.Cheese},
                                    new Ingredient {IngredientOption = IngredientEnum.Tomatosauce},
                                    new Ingredient {IngredientOption = IngredientEnum.Kebab},
                                    new Ingredient {IngredientOption = IngredientEnum.Mushrooms},
                                    new Ingredient {IngredientOption = IngredientEnum.Onion},
                                    new Ingredient {IngredientOption = IngredientEnum.Pepperoni},
                                    new Ingredient {IngredientOption = IngredientEnum.Salad},
                                    new Ingredient {IngredientOption = IngredientEnum.Tomato},
                                    new Ingredient {IngredientOption = IngredientEnum.KebabSauce}
                                },
                                BasePrice = 105
                            },

                            new()
                            {
                                MenuNumber = 4, Name = "Quatro Stagioni", PizzaIngredients =
                                {
                                    new Ingredient {IngredientOption = IngredientEnum.Cheese},
                                    new Ingredient {IngredientOption = IngredientEnum.Tomatosauce},
                                    new Ingredient {IngredientOption = IngredientEnum.Ham},
                                    new Ingredient {IngredientOption = IngredientEnum.Shrimps},
                                    new Ingredient {IngredientOption = IngredientEnum.Clams},
                                    new Ingredient {IngredientOption = IngredientEnum.Mushrooms},
                                    new Ingredient {IngredientOption = IngredientEnum.Artichoke}
                                },
                                BasePrice = 115
                            }
                        };

                        var sodaMenu = new List<Soda>
                        {
                            new() {MenuNumber = 1, Name = "Coca Cola", Price = 20},
                            new() {MenuNumber = 2, Name = "Fanta", Price = 20},
                            new() {MenuNumber = 3, Name = "Sprite", Price = 25}
                        };

                        var extraIngredients = new List<Ingredient>();
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 1, IngredientOption = IngredientEnum.Ham, Price = 10});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 2, IngredientOption = IngredientEnum.Pineapple, Price = 10});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 3, IngredientOption = IngredientEnum.Mushrooms, Price = 10});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 4, IngredientOption = IngredientEnum.Onion, Price = 10});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 5, IngredientOption = IngredientEnum.KebabSauce, Price = 10});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 6, IngredientOption = IngredientEnum.Shrimps, Price = 15});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 7, IngredientOption = IngredientEnum.Clams, Price = 15});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 8, IngredientOption = IngredientEnum.Artichoke, Price = 15});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 9, IngredientOption = IngredientEnum.Kebab, Price = 20});
                        extraIngredients.Add(new Ingredient
                            {MenuNumber = 10, IngredientOption = IngredientEnum.Coriander, Price = 20});

                        var menu = new Menu();
                        menu.PizzaMenu = pizzaMenu;
                        menu.SodaMenu = sodaMenu;
                        menu.IngredientMenu = extraIngredients;

                        context.Add(menu);
                        context.SaveChanges();
                    }
                }
            }

            return @this;
        }
    }
}