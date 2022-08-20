using System;
using System.Collections.Generic;
using System.Linq;
using GameDBLibrary;


/**************************************************************************************
*
*
*                     THIS IS A GENERATED FILE! DO NOT EDIT!
*
*
**************************************************************************************/

namespace GameDBProductData
{
    public class GameData : RowBase
    {
#pragma warning disable 0414
		private GameDB m_gameDB = null;
#pragma warning restore 0414

        public GameData(string key, GameDB gameDB) : base(key) {
			m_gameDB = gameDB;
		}

        public string Genre1Val
        {
		    get { return (System.String)Convert.ChangeType(GetValue(GameDataSchema.FieldGenre1), typeof(System.String)); }
        }

        public string Genre2Val
        {
		    get { return (System.String)Convert.ChangeType(GetValue(GameDataSchema.FieldGenre2), typeof(System.String)); }
        }

        public int RatingVal
        {
		    get { return (System.Int32)Convert.ChangeType(GetValue(GameDataSchema.FieldRating), typeof(System.Int32)); }
        }

        public int TrendVal
        {
		    get { return (System.Int32)Convert.ChangeType(GetValue(GameDataSchema.FieldTrend), typeof(System.Int32)); }
        }

    }
}
