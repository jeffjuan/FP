jconfirm.defaults = {
    title: 'Hello',
    content: 'Are you sure to continue?',
    contentLoaded: function () {
    },
    icon: '',
    confirmButton: '確定',
    cancelButton: '關閉',
    confirmButtonClass: 'btn-default',
    cancelButtonClass: 'btn-default',
    theme: 'white',
    animation: 'scale',
    closeAnimation: 'scale',
    animationSpeed: 500,
    animationBounce: 1.2,
    keyboardEnabled: false,
    rtl: false,
    confirmKeys: [13], // ENTER key
    cancelKeys: [27], // ESC key
    container: 'body',
    confirm: function () {
    },
    cancel: function () {
    },
    backgroundDismiss: false,
    autoClose: false,
    closeIcon: null,
    columnClass: 'col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3 col-xs-10 col-xs-offset-1',
    onOpen: function () {
    },
    onClose: function () {
    },
    onAction: function () {
    }
};