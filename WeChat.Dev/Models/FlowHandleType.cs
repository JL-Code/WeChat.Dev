namespace WeChat.Dev.Models
{
    public enum FlowHandleType
    {
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 0,
        /// <summary>
        /// 不同意
        /// </summary>
        Disagree = 1,
        /// <summary>
        /// 发起协商
        /// </summary>
        Assign = 2,
        /// <summary>
        /// 授权交办
        /// </summary>
        Transfer = 3,
        /// <summary>
        /// 会签
        /// </summary>
        JointSignature = 4,
        /// <summary>
        /// 重新发起
        /// </summary>
        AgainInitiate = 5,
        /// <summary>
        /// 作废
        /// </summary>
        Cancel = 6,
        /// <summary>
        /// 发起
        /// </summary>
        Begin = 7,
        /// <summary>
        /// 结束
        /// </summary>
        End = 8,
        /// <summary>
        /// 撤回
        /// </summary>
        Recall = 9,
        /// <summary>
        /// 催办
        /// </summary>
        Urge = 10,
        /// <summary>
        /// 回复协商
        /// </summary>
        AnswerAssign = 21
    }
}