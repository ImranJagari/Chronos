using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class CreditCardType
    {
        public const byte CREDIT_NUM_MAX_LEN = 12;
        public CreditCardTypeEnum type;
        public int limit;
        public int current_limit;
        public int recharge;
        public int pay_recharge;
        public int preferential;
        public int one_day_consume;
        public int total_consume;
        public uint last_consume_date;
        public int one_day_trade;
        public uint last_trade_date;
        public int recharged_money;
        public int trade_point;
        public int recharged_reward;

        public CreditCardType(CreditCardTypeEnum type, int limit, int current_limit, int recharge, int pay_recharge, int preferential, int one_day_consume,
            int total_consume, uint last_consume_date, int one_day_trade, uint last_trade_date, int recharged_money, int trade_point, int recharged_reward)
        {
            this.type = type;
            this.limit = limit;
            this.current_limit = current_limit;
            this.recharge = recharge;
            this.pay_recharge = pay_recharge;
            this.preferential = preferential;
            this.one_day_consume = one_day_consume;
            this.total_consume = total_consume;
            this.last_consume_date = last_consume_date;
            this.one_day_trade = one_day_trade;
            this.recharged_money = recharged_money;
            this.trade_point = trade_point;
            this.recharged_reward = recharged_reward;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
            writer.WriteInt(limit);
            writer.WriteInt(current_limit);
            writer.WriteInt(recharge);
            writer.WriteInt(pay_recharge);
            writer.WriteInt(preferential);
            writer.WriteInt(one_day_consume);
            writer.WriteInt(total_consume);
            writer.WriteUInt(last_consume_date);
            writer.WriteInt(one_day_trade);
            writer.WriteUInt(last_trade_date);
            writer.WriteInt(recharged_money);
            writer.WriteInt(trade_point);
            writer.WriteInt(recharged_reward);
        }
    }
}
