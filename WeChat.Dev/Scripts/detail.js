
var loading = weui.loading();

init();
galleryInit();

function init() {
    axios.get('/Scripts/data/timeline.data.json').then(function (response) {
        var data = response.data;
        var formHtml = template('tpl_formtable', data);
        var attachmentHtml = template('tpl_attachment', data);
        var timelineHtml = createTimeLine(data.FlowInstance, data.NodeExtends);
        sessionStorage.setItem("flow_data", JSON.stringify({ fi: data.FlowInstance, step: data.CurrentStep }));
        document.getElementById('form_container').innerHTML = formHtml;
        document.getElementById('form_attachment').innerHTML = attachmentHtml;
        document.getElementById('form_timeline').innerHTML = timelineHtml;
        utils.setTitle(data.FlowInstance.Title);
        setTimeout(function () {
            utils.renderTimego('.comments-time');
            loading.hide();
        }, 100)
    }).catch(function (err) {
        loading.hide();
    });
}

function agree() {
    handle("同意", enums.handleType.Assign);
}

//流程不同意处理
function disaccord() {
    weui.actionSheet([{
        label: ' 打回到发起',
        onClick: function () {
            handle("不同意", enums.handleType.ToStart);
        }
    }, {
        label: '打回到上一步',
        onClick: function () {
            handle("不同意", enums.handleType.ToPrev);
        }
    }], [{
        label: '取消',
        onClick: function () {
            console.log('取消')
        }
    }])
}

//更多操作
function moreActions() {
    weui.actionSheet([{
        label: '授权交办',
        onClick: function () {
            handle("授权交办", enums.handleType.Transfer);
        }
    },
    {
        label: '发起协商',
        onClick: function () {
            handle("", enums.handleType.LaunchConsult);
        }
    },
    {
        label: '作废',
        onClick: function () {
            handle("作废", enums.handleType.Cancel);
        }
    }
    ], [{
        label: '取消',
        onClick: function () {
            console.log('取消了')
        }
    }]);
}

//生成时间轴
function createTimeLine(flowInstance, nodeExtends) {
    var stepsVM = [];
    var exitDo = false;
    try {
        var steps = flowInstance.StepInstanceList;
        var nodes = flowInstance.NodeInstanceList;
        //为步骤附加节点信息
        for (var s = 0; s < steps.length; s++) {
            var step = steps[s];
            step.nodes = [];
            for (var n = 0; n < nodes.length; n++) {
                if (step.StepInstanceGuid != nodes[n].StepInstanceGuid)
                    continue;
                var node = nodes[n];
                //获取节点的扩展信息 交办 和 协商
                if (step.CanAssign || step.CanTransfer) {
                    node.extendNodeInfo = getExtendNodeInfo(node, nodeExtends);
                }
                step.nodes.push(node);
            }
        }
        //拼接时间轴对象
        for (var si = 0; si < steps.length; si++) {
            var tempSteps = [];
            var tempStep = steps[si];
            var surplus = [];
            var virtualStep = null;

            if (exitDo) break;
            switch (tempStep.StepType) {
                case enums.stepCategory.start:
                    tempStep.patch = "first";
                    tempStep.bgColor = "bg-green";
                    tempStep.bColor = "bg-green";
                    if (si != 0) {
                        tempStep.patch = "";
                        tempStep.noBefore = false;
                    }
                    break;
                case enums.stepCategory.end:
                    tempStep.patch = "last";
                    tempStep.noBefore = false;
                    tempStep.bgColor = "bg-gray";
                    tempStep.bColor = "bg-gray";
                    break;
                default:
                    tempStep.bgColor = "bg-gray";
                    tempStep.bColor = "bg-gray";
                    break;
            }
            if (tempStep.nodes && tempStep.nodes.length)
                for (var ni = 0; ni < tempStep.nodes.length; ni++) {
                    var tempNode = tempStep.nodes[ni];
                    tempNode.FlowHandleTypeName = getHandleMethodName(tempNode.FlowHandleType);
                    if (tempNode.HandleMode == 2 && tempNode.NodeStatus != -2) continue;//系统处理的节点不显示
                    //1.节点为已处理
                    //2.步骤处理模式为串行
                    if (tempNode.NodeStatus != 0 && tempNode.NodeStatus != 1) {//已处理
                        virtualStep = Object.assign({}, tempStep);
                        //状态 步骤头 节点
                        virtualStep.bgColor = "bg-green";
                        virtualStep.bColor = "bg-green";
                        if (tempNode.NodeStatus == -2) {
                            virtualStep.bgColor = "bg-red";
                            virtualStep.bColor = "bg-red";
                            virtualStep.patch = "last";
                            virtualStep.toVoid = true;
                            //终止下一步解析
                            exitDo = true;
                        }
                        virtualStep.headerHide = ni ? true : false;
                        virtualStep.nodes = [tempNode];
                        tempSteps.push(virtualStep);

                    } else if (tempStep.HandleMode == enums.stepHandleMethod.serial) {//串行未处理
                        virtualStep = Object.assign({}, tempStep);
                        //状态 步骤头 节点
                        if (tempNode.NodeStatus == enums.nodeState.toWait) {
                            virtualStep.bgColor = "bg-yellow";
                            virtualStep.bColor = "bg-yellow";
                        }
                        virtualStep.headerHide = ni ? true : false;
                        virtualStep.nodes = [tempNode];
                        tempSteps.push(virtualStep);

                    } else {//并行且未处理
                        tempNode.headerHide = ni ? true : false;
                        tempNode.bgColor = "bg-gray";
                        tempNode.bColor = "bg-gray";
                        if (tempNode.NodeStatus == enums.nodeState.toWait) {
                            tempNode.bgColor = "bg-yellow";
                            tempNode.bColor = "bg-yellow";
                        }
                        surplus.push(tempNode);
                    }
                    if (exitDo) break;
                }

            //并行且未处理的 特殊处理
            if (surplus.length) {
                virtualStep = tempStep;
                virtualStep.nodes = surplus;
                virtualStep.each = true;
                virtualStep.headerHide = surplus[0].headerHide;
                virtualStep.bgColor = surplus[0].bgColor;
                tempSteps.push(virtualStep);
            }

            stepsVM = stepsVM.concat(tempSteps);
        }
        return template('tpl_timeline', { steps: stepsVM });
    } catch (e) {
        console.error(e);
        return "";
    }
}

//获取扩展节点信息
function getExtendNodeInfo(node, nodeExtend) {
    var extendNodes = {};
    if (nodeExtend && nodeExtend.length)
        for (var k = 0; k < nodeExtend.length; k++) {
            var currExtNode = nodeExtend[k];
            //协商和回复协商
            if (currExtNode.NodeExtendType == 3 || currExtNode.NodeExtendType == 2) {
                if (currExtNode.NodeInstanceGuid != node.NodeInstanceGuid || !currExtNode.HandleDatetime) continue;
                var extNodes = extendNodes[node.NodeInstanceGuid] || [];
                switch (currExtNode.NodeExtendType) {
                    case 2: //发起

                        var extNode = {
                            title: '发起协商',
                            content: currExtNode.HandlerSuggestion,
                            senderId: currExtNode.HandlerGuid,
                            recipientId: currExtNode.AuditorGuid,
                            sender: currExtNode.HandlerName,
                            recipient: currExtNode.AuditorName,
                            extra: currExtNode
                        };
                        extNodes.push(extNode);
                        extendNodes[node.NodeInstanceGuid] = extNodes;
                        break;
                    case 3: //回复

                        extNodes.forEach(function (extNode, index, self) {
                            if (extNode.recipientId == currExtNode.HandlerGuid) {
                                if (!extNode.comments)
                                    extNode.comments = [];
                                var comment = {
                                    title: '回复',
                                    content: currExtNode.HandlerSuggestion,
                                    senderId: currExtNode.HandlerGuid,
                                    recipientId: currExtNode.AuditorGuid,
                                    sender: currExtNode.HandlerName,
                                    recipient: currExtNode.AuditorName,
                                    extra: currExtNode
                                };
                                extNode.comments.push(comment);
                            }
                        });
                        break;
                    default:
                        break;

                }
            }
            else if (currExtNode.NodeExtendType >= 5 && currExtNode.NodeExtendType <= 8) {//作废
                if (currExtNode.StepInstanceGuid == node.StepInstanceGuid) {
                    return currExtNode;
                }
            }
        }
    return extendNodes;
}

//获取处理方法名称
function getHandleMethodName(method) {
    switch (method) {
        /// <summary>
        /// 同意
        /// </summary>

        case 0:
            return "同意";
            break;

        /// <summary>
        /// 不同意
        /// </summary>

        case 1:
            return "不同意";
            break;
        /// <summary>
        /// 发起协商
        /// </summary>

        case 2:
            return "发起协商";
            break;
        /// <summary>
        /// 授权交办
        /// </summary>

        case 3:
            return "授权交办";
            break;
        /// <summary>
        /// 会签
        /// </summary>

        case 4:
            return "会签";
            break;
        /// <summary>
        /// 重新发起
        /// </summary>

        case 5:
            return "重新发起";

            break;
        /// <summary>
        /// 作废
        /// </summary>

        case 6:
            return "作废";

            break;
        /// <summary>
        /// 发起
        /// </summary>

        case 7:
            return "发起";

            break;
        /// <summary>
        /// 结束
        /// </summary>

        case 8:
            return "结束";

            break;
        /// <summary>
        /// 撤回
        /// </summary>

        case 9:
            return "同意";
            break;
        /// <summary>
        /// 回复协商
        /// </summary>

        case 21:
            return "回复协商";
            break;
        default:
            return "";
            break;

    }
}

//附件图片幻灯片组件查看
function galleryInit() {
    var initPhotoSwipeFromDOM = function (gallerySelector) {
        // parse slide data (url, title, size ...) from DOM elements
        // (children of gallerySelector)
        var parseThumbnailElements = function (el) {
            var thumbElements = el.querySelectorAll('.figure'),
                items = [],
                figureEl,
                size,
                item;
            for (var i = 0; i < thumbElements.length; i++) {

                figureEl = thumbElements[i]; // <figure> element
                size = figureEl.dataset.size.split('x');

                // create slide object
                item = {
                    src: figureEl.dataset.href,
                    w: parseInt(size[0], 10),
                    h: parseInt(size[1], 10)
                };
                item.el = figureEl; // save link to element for getThumbBoundsFn
                items.push(item);
            }
            return items;
        };

        // find nearest parent element 查找最近的父元素
        var closest = function closest(el, fn) {
            return el && (fn(el) ? el : closest(el.parentNode, fn));
        };

        // triggers when user clicks on thumbnail 当用户点击缩略图我们触发图片预览
        var onThumbnailsClick = function (e) {

            e = e || window.event;
            e.preventDefault ? e.preventDefault() : e.returnValue = false;

            var eTarget = e.target || e.srcElement;

            // find root element of slide 查找幻灯片的根元素
            var clickedListItem = closest(eTarget, function (el) {
                return (el.classList && el.classList.contains('figure'));
            });

            if (!clickedListItem) {
                return;
            }

            // find index of clicked item by looping through all child nodes
            // alternatively, you may define index via data- attribute
            var clickedGallery = clickedListItem.parentNode,
                childNodes = clickedListItem.parentNode.childNodes,
                numChildNodes = childNodes.length,
                nodeIndex = 0,
                index;

            for (var i = 0; i < numChildNodes; i++) {
                if (childNodes[i].nodeType !== 1) {
                    continue;
                }

                if (childNodes[i] === clickedListItem) {
                    index = nodeIndex;
                    break;
                }
                nodeIndex++;
            }


            if (index >= 0) {
                // open PhotoSwipe if valid index found
                openPhotoSwipe(index, clickedGallery);
            }
            return false;
        };

        // 从URL解析图片索引和图库索引 (#&pid=1&gid=2)
        var photoswipeParseHash = function () {

            var hash = window.location.hash.substring(1),
                params = {};

            if (hash.length < 5) {
                return params;
            }

            var vars = hash.split('&');
            for (var i = 0; i < vars.length; i++) {
                if (!vars[i]) {
                    continue;
                }
                var pair = vars[i].split('=');
                if (pair.length < 2) {
                    continue;
                }
                params[pair[0]] = pair[1];
            }

            if (params.gid) {
                params.gid = parseInt(params.gid, 10);
            }

            return params;
        };


        var openPhotoSwipe = function (index, galleryElement, disableAnimation, fromURL) {
            var pswpElement = document.querySelectorAll('.pswp')[0],
                gallery,
                options,
                items;

            items = parseThumbnailElements(galleryElement);

            // define options (if needed)
            options = {

                // define gallery index (for URL)
                galleryUID: galleryElement.getAttribute('data-pswp-uid')

                // getThumbBoundsFn: function (index) {
                //     // See Options -> getThumbBoundsFn section of documentation for more info
                //     var thumbnail = items[index].el.querySelector('.img'), // find thumbnail
                //         pageYScroll = window.pageYOffset || document.documentElement.scrollTop,
                //         rect = thumbnail.getBoundingClientRect();
                //     return {x: rect.left, y: rect.top + pageYScroll, w: rect.width};
                // }

            };
            // PhotoSwipe opened from URL
            if (fromURL) {
                if (options.galleryPIDs) {
                    // parse real index when custom PIDs are used
                    // http://photoswipe.com/documentation/faq.html#custom-pid-in-url
                    for (var j = 0; j < items.length; j++) {
                        if (items[j].pid == index) {
                            options.index = j;
                            break;
                        }
                    }
                } else {
                    // in URL indexes start from 1
                    options.index = parseInt(index, 10) - 1;
                }
            } else {
                options.index = parseInt(index, 10);
            }

            // exit if index not found
            if (isNaN(options.index)) {
                return;
            }

            if (disableAnimation) {
                options.showAnimationDuration = 0;
            }

            // Pass data to PhotoSwipe and initialize it
            gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
            gallery.init();
        };

        // loop through all gallery elements and bind events
        var galleryElements = document.querySelectorAll(gallerySelector);

        for (var i = 0, l = galleryElements.length; i < l; i++) {

            galleryElements[i].setAttribute('data-pswp-uid', i + 1);
            galleryElements[i].onclick = onThumbnailsClick;
        }

        // Parse URL and open gallery if it contains #&pid=3&gid=1
        var hashData = photoswipeParseHash();
        if (hashData.pid && hashData.gid) {
            openPhotoSwipe(hashData.pid, galleryElements[hashData.gid - 1], true, true);
        }

        window.gallery = {
            onThumbnailsClick: onThumbnailsClick
        }
    };
    initPhotoSwipeFromDOM('#form_attachment');
}

//处理流程
function handle(suggestion, type) {
    sessionStorage.setItem("extra", JSON.stringify({
        suggestion: suggestion,
        handleType: type
    }));
    utils.jumpLink("/home/BusinessHandlePage");
}

/**
 * @description 附件预览
 */
function attachmentPreview(target) {
    var data = getAttachmentData(target);
    console.log(data);
    utils.preview(data);
}

function getAttachmentData(target) {
    return {
        isphoto: utils.dateset(target, "isphoto", true),
        fileid: utils.dateset(target, "fileid"),
        userid: utils.dateset(target, "userid"),
        href: utils.dateset(target, "href"),
        doctype: utils.dateset(target, "doctype")
    }
}