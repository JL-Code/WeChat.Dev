﻿(function (host, factory) {
    host.enums = factory();
}(window, function () {
    return {
        stepCategory: {
            /// <summary>
            /// 开始
            /// </summary>
            start: 0,
            /// <summary>
            /// 审批
            /// </summary>
            approval: 1,
            /// <summary>
            /// 会签
            /// </summary>
            jointSignature: 2,
            /// <summary>
            /// 结束
            /// </summary>
            end: 3
        },
        stepHandleMethod: {
            /// <summary>
            /// 串行
            /// </summary>
            serial: 1,
            /// <summary>
            /// 并行
            /// </summary>
            parallel: 2,
        },
        nodeState: {

            /// <summary>
            /// 未激活。是默认状态。
            /// 有两个时候发生激活：1）系统激活，当期步骤的节点状态改为待办时，记录系统激活时间
            ///                    2）人工激活，当待办人查看流程表单的时候，记录人工激活时间
            /// </summary>
            unActivate: 0,

            /// <summary>
            /// 待办。流转中，有且只有一个步骤的节点（可能是多个节点）状态是待办
            /// </summary>
            toWait: 1,

            /// <summary>
            /// 已处理。正常处理通过的节点状态，
            /// </summary>
            processed: 2,

            /// <summary>
            /// 已打回。自己不办理，将流程打回给上一步或发起人，
            /// </summary>
            toBack: -1,

            /// <summary>
            /// 已作废。终止流转.
            /// </summary>
            toVoid: -2,
        }
    }
}))