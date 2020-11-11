using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Enumerations
{
    public enum FormState
    {
        NotHandled,
        Approve,
        Done,
        Reject
    }


    public enum JudgementState
    {
        NotHandled,
        Approve,
        Reject,

    }


    public enum NotifyState
    {
        Pending,
        Done,
        Expired,
        System
    }


    public enum OrderJudgement
    {
        NonAction,
        Default,
        First,
    }


    public class NameMappingStatus
    {
        public static Dictionary<FormState, string> FromStateDictionary = new Dictionary<FormState, string>
        {
            { FormState.NotHandled, "Chờ duyệt"},
            { FormState.Approve, "Đang xử lý"},
            { FormState.Done, "Đã xử lý"},
            { FormState.Reject, "Từ chối"},
        };


        public static Dictionary<JudgementState, string> JudgementStateDictionary = new Dictionary<JudgementState, string>
        {
            { JudgementState.NotHandled, "Chưa duyệt"},
            { JudgementState.Approve, "Đồng ý"},
            { JudgementState.Reject, "Từ chối"},
        };


        public static Dictionary<NotifyState, string> NotifyStateDictionary = new Dictionary<NotifyState, string>
        {
            { NotifyState.Pending, "Chưa xử lý"},
            { NotifyState.Done, "Đã xử lý"},
            { NotifyState.Expired, "Hết hạn"},
            { NotifyState.System, "Thông báo từ hệ thống"},
        };
    }


    public class NumberMappingStatus
    {
        public static Dictionary<OrderJudgement, int> OrderJudgementDictionary = new Dictionary<OrderJudgement, int>
        {
            { OrderJudgement.NonAction, -1},
            { OrderJudgement.Default, 0},
            { OrderJudgement.First, 1}
        };
    }
}
