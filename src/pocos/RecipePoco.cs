﻿using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;

namespace GilGoblin.pocos
{
    public class RecipePoco
    {
        public int recipeID { get; set; }
        public int targetItemID { get; set; }
        public int iconID { get; set; }
        public int resultQuantity { get; set; }
        public bool canHq { get; set; }
        public bool canQuickSynth { get; set; }
        public List<IngredientPoco> ingredients { get; set; } = new List<IngredientPoco>();
        public RecipePoco() { }

        [JsonConstructor]
        public RecipePoco(bool CanQuickSynth, bool CanHq, int ItemResultTargetID, int ID, int IconID, int AmountResult,
            int AmountIngredient0, int AmountIngredient1,
            int AmountIngredient2,
            int AmountIngredient3,
            int AmountIngredient4,
            int AmountIngredient5,
            int AmountIngredient6,
            int AmountIngredient7,
            int AmountIngredient8,
            int AmountIngredient9,
            int ItemIngredient0TargetID,
            int ItemIngredient1TargetID,
            int ItemIngredient2TargetID,
            int ItemIngredient3TargetID,
            int ItemIngredient4TargetID,
            int ItemIngredient5TargetID,
            int ItemIngredient6TargetID,
            int ItemIngredient7TargetID,
            int ItemIngredient8TargetID,
            int ItemIngredient9TargetID) : base()
        {
            this.canHq = CanHq;
            this.iconID = IconID;
            this.targetItemID = ItemResultTargetID;
            this.recipeID = ID;
            this.resultQuantity = AmountResult;
            this.canQuickSynth = CanQuickSynth;
            if (AmountIngredient0 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient0TargetID, AmountIngredient0, this.recipeID));
            }
            if (AmountIngredient1 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient1TargetID, AmountIngredient1, this.recipeID));
            }
            if (AmountIngredient2 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient2TargetID, AmountIngredient2, this.recipeID));
            }
            if (AmountIngredient3 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient3TargetID, AmountIngredient3, this.recipeID));
            }
            if (AmountIngredient4 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient4TargetID, AmountIngredient4, this.recipeID));
            }
            if (AmountIngredient5 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient5TargetID, AmountIngredient5, this.recipeID));
            }
            if (AmountIngredient6 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient6TargetID, AmountIngredient6, this.recipeID));
            }
            if (AmountIngredient7 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient7TargetID, AmountIngredient7, this.recipeID));
            }
            if (AmountIngredient8 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient8TargetID, AmountIngredient8, this.recipeID));
            }
            if (AmountIngredient9 > 0)
            {
                ingredients.Add(new IngredientPoco(ItemIngredient9TargetID, AmountIngredient9, this.recipeID));
            }
        }

        public RecipePoco(RecipePoco old)
        {
            this.recipeID = old.recipeID;
            this.targetItemID = old.targetItemID;
            this.iconID = old.iconID;
            this.resultQuantity = old.resultQuantity;
            this.canHq = old.canHq;
            this.canQuickSynth = old.canQuickSynth;
            this.ingredients = old.ingredients;
        }
    }
}