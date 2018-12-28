using Chronos.Protocol.Types;
using Chronos.Server.Databases.Items;
using Chronos.Server.Network;

namespace Chronos.Server.Game.Inventories.Items
{
    public class BagItem
    {
        public BagItem(BagItemRecord record)
        {
            Record = record;
        }

        public BagItemRecord Record { get; }

        public int Id
        {
            get => Record.Id;
            set => Record.Id = value;
        }

        public int OwnerId
        {
            get => Record.OwnerId;
            set => Record.OwnerId = value;
        }

        public byte Slot
        {
            get => Record.Slot;
            set => Record.Slot = value;
        }

        public uint ItemId
        {
            get => Record.ItemId;
            set => Record.ItemId = value;
        }

        public uint Word
        {
            get => Record.Word;
            set => Record.Word = value;
        }

        public int Option
        {
            get => Record.Option;
            set => Record.Option = value;
        }

        public uint Quantity
        {
            get => Record.Quantity;
            set => Record.Quantity = value;
        }

        public int Hitpoint
        {
            get => Record.Hitpoint;
            set => Record.Hitpoint = value;
        }

        public int MaxHitpoint
        {
            get => Record.MaxHitpoint;
            set => Record.MaxHitpoint = value;
        }

        public byte ItemResist
        {
            get => Record.ItemResist;
            set => Record.ItemResist = value;
        }

        public byte ItemResistAbilityOption
        {
            get => Record.ItemResistAbilityOption;
            set => Record.ItemResistAbilityOption = value;
        }

        public int KeepTime
        {
            get => Record.KeepTime;
            set => Record.KeepTime = value;
        }

        public byte ItemLock
        {
            get => Record.ItemLock;
            set => Record.ItemLock = value;
        }

        public int BindEndTime
        {
            get => Record.BindEndTime;
            set => Record.BindEndTime = value;
        }

        public byte Stability
        {
            get => Record.Stability;
            set => Record.Stability = value;
        }

        public byte Quality
        {
            get => Record.Quality;
            set => Record.Quality = value;
        }

        public sbyte AbilityRate
        {
            get => Record.AbilityRate;
            set => Record.AbilityRate = value;
        }

        public int UseTime
        {
            get => Record.UseTime;
            set => Record.UseTime = value;
        }

        public int BuyTm
        {
            get => Record.BuyTm;
            set => Record.BuyTm = value;
        }

        public int Price
        {
            get => Record.Price;
            set => Record.Price = value;
        }

        public int PriceToken
        {
            get => Record.PriceToken;
            set => Record.PriceToken = value;
        }

        public int PriceFreeToken
        {
            get => Record.PriceFreeToken;
            set => Record.PriceFreeToken = value;
        }

        public ItemType GetItemType()
        {
            return new ItemType(Slot, ItemId, Word, Option);
        }

        public ItemElementType GetItemElementType()
        {
            return new ItemElementType(Slot, ItemId, (uint) Id, (int) Quantity,
                Hitpoint, MaxHitpoint, Word, (byte) Option, ItemResist,
                ItemResistAbilityOption, KeepTime, ItemLock, BindEndTime,
                Stability, Quality, AbilityRate, UseTime, BuyTm, Price,
                PriceToken, PriceFreeToken, SimpleServer.ServerId, new short[1]{ 25 });
        }
    }
}