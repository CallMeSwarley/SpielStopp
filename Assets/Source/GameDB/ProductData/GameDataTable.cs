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
    public class GameDataTable : TableBase
    {
        public GameDataTable(Func<string, RowBase> rowFactory) : base(GameDataSchema.TableName, KeyType.@string, null, rowFactory) {
            m_fields = new Dictionary<string, FieldBase>() {
                { GameDataSchema.FieldGenre1, new FieldBase(GameDataSchema.FieldGenre1, FieldType.@string, false, null) },
                { GameDataSchema.FieldGenre2, new FieldBase(GameDataSchema.FieldGenre2, FieldType.@string, false, null) },
                { GameDataSchema.FieldRating, new FieldBase(GameDataSchema.FieldRating, FieldType.@int, false, null) },
                { GameDataSchema.FieldTrend, new FieldBase(GameDataSchema.FieldTrend, FieldType.@int, false, null) }
            };
        }

        public GameData GetByKey(string key) { return m_data[key] as GameData; }

        public Dictionary<string, GameData> GetRows() { return m_data.ToDictionary(entry => entry.Key, entry => (GameData)entry.Value); }
    }
}
