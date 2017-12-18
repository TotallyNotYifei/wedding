//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Config.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.Overcooked
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// A collection of static configurations
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Used for initialization
        /// </summary>
        static Config()
        {
            DirectionToAnimatorParam = new Dictionary<DirectionEnum, string>();
            DirectionToAnimatorParam[DirectionEnum.Up] = "IsUp";
            DirectionToAnimatorParam[DirectionEnum.Left] = "IsLeft";
            DirectionToAnimatorParam[DirectionEnum.Down] = "IsDown";
            DirectionToAnimatorParam[DirectionEnum.Right] = "IsRight";

            FaceDirectionOffset = new Dictionary<DirectionEnum, Vector3>();
            FaceDirectionOffset[DirectionEnum.Up] = new Vector3(0, 0.17f);
            FaceDirectionOffset[DirectionEnum.Left] = new Vector3(-0.32f, -0.08f);
            FaceDirectionOffset[DirectionEnum.Down] = new Vector3(0, -0.31f);
            FaceDirectionOffset[DirectionEnum.Right] = new Vector3(0.32f, -0.08f);

            FinishedBurger = new List<IngredientEnum>();
            FinishedBurger.Add(IngredientEnum.BurgerBun);
            FinishedBurger.Add(IngredientEnum.Meat);
            FinishedBurger.Add(IngredientEnum.Lettunce);
            FinishedBurger.Add(IngredientEnum.Tomato);

            FinishedSalad = new List<IngredientEnum>();
            FinishedSalad.Add(IngredientEnum.Lettunce);
            FinishedSalad.Add(IngredientEnum.Tomato);

            FinishedSoup = new List<IngredientEnum>();
            FinishedSoup.Add(IngredientEnum.OnionSoup);

            Recipes = new List<List<IngredientEnum>>();
            Recipes.Add(FinishedBurger);
            Recipes.Add(FinishedSalad);
            Recipes.Add(FinishedSoup);

            HoldItemOffset = new Dictionary<DirectionEnum, Vector3>();
            HoldItemOffset[DirectionEnum.Down] = new Vector3(0, -0.06f);
            HoldItemOffset[DirectionEnum.Left] = new Vector3(-0.25f, 0f);
            HoldItemOffset[DirectionEnum.Up] = new Vector3(0, 0.14f);
            HoldItemOffset[DirectionEnum.Right] = new Vector3(0.25f, 0f);

            HoldItemLayer = new Dictionary<DirectionEnum, int>();
            HoldItemLayer[DirectionEnum.Down] = 2;
            HoldItemLayer[DirectionEnum.Left] = 2;
            HoldItemLayer[DirectionEnum.Up] = -1;
            HoldItemLayer[DirectionEnum.Right] = 2;
        }

        /// <summary>
        /// How fast the characters can move
        /// </summary>
        public const float MovementSpeed = 2.0f;

        /// <summary>
        /// Size of the grid
        /// </summary>
        public const float GridSizeX = 0.36f;
        public const float GridSizeY = 0.27f;

        /// <summary>
        /// How tall to place the item when item is on a counter
        /// </summary>
        public const float ItemPlacementHeight = 0.125f;

        /// <summary>
        /// A dictionary of directionEnum => Animator parameter name
        /// </summary>
        public static Dictionary<DirectionEnum, string> DirectionToAnimatorParam;

        /// <summary>
        /// A dictionary of directionEnum => Vector3 as direction
        /// </summary>
        public static Dictionary<DirectionEnum, Vector3> FaceDirectionOffset;

        /// <summary>
        /// A dictionary of holding item => item offset
        /// </summary>
        public static Dictionary<DirectionEnum, Vector3> HoldItemOffset;

        /// <summary>
        /// A dictionary of hel item => their sorting layer
        /// </summary>
        public static Dictionary<DirectionEnum, int> HoldItemLayer;

        /// <summary>
        /// How far away can the player pick up/chop/etc
        /// </summary>
        public const float TargetRange = 0.30f;

        /// <summary>
        /// What's considered a finished burger
        /// </summary>
        public static List<IngredientEnum> FinishedBurger;

        /// <summary>
        /// What's considered a finished soup
        /// </summary>
        public static List<IngredientEnum> FinishedSoup;

        /// <summary>
        /// What's considered a finished salad
        /// </summary>
        public static List<IngredientEnum> FinishedSalad;

        /// <summary>
        /// All of the recipes
        /// </summary>
        public static List<List<IngredientEnum>> Recipes;
    }
}
