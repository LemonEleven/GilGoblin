using System.Collections.Generic;
using System.Linq;

namespace GilGoblin.Database.Pocos.Extensions;

public static class RecipePocoExtension
{
    public static List<IngredientPoco> GetIngredientsList(this RecipePoco poco) =>
    [
        new (poco.ItemIngredient0TargetId, poco.AmountIngredient0, poco.Id),
        new (poco.ItemIngredient1TargetId, poco.AmountIngredient1, poco.Id),
        new (poco.ItemIngredient2TargetId, poco.AmountIngredient2, poco.Id),
        new (poco.ItemIngredient3TargetId, poco.AmountIngredient3, poco.Id),
        new (poco.ItemIngredient4TargetId, poco.AmountIngredient4, poco.Id),
        new (poco.ItemIngredient5TargetId, poco.AmountIngredient5, poco.Id),
        new (poco.ItemIngredient6TargetId, poco.AmountIngredient6, poco.Id),
        new (poco.ItemIngredient7TargetId, poco.AmountIngredient7, poco.Id),
        new (poco.ItemIngredient8TargetId, poco.AmountIngredient8, poco.Id),
        new (poco.ItemIngredient9TargetId, poco.AmountIngredient9, poco.Id)
    ];

    public static List<IngredientPoco> GetActiveIngredients(this RecipePoco poco) =>
        poco.GetIngredientsList().Where(i => i.Quantity > 0).ToList();
}