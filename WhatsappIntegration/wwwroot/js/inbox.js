
$(document).ready(function () {
    $(".chatList-item").click(function () {
        let chatId = $(this).attr('chat-id');
        let person = $(this).attr('person');
        let phone = $(this).attr('phone');
        $.ajax({
            url: '/Inbox/ChatBox?chatId=' + chatId + '&person=' + person + '&phone=' + phone,
            type: 'POST',
            dataType: 'html',
            success: function (data) {
                $("#chatbox-container").html(data);
            },
            error: function (response) {
                if (response.status == '401') {
                    window.location.href = '/Account/Login?ReturnUrl=%2FInbox%2FIndex';
                }
                else {
                    alert("Error Occurred.");
                }
            }
        });
        let chatlistItems = document.getElementsByClassName('chatList-item');
        for (var i = 0; i < chatlistItems.length; i++) {
            if (chatId == chatlistItems[i].getAttribute('chat-id')) {
                chatlistItems[i].className = "chatList-item selected";
                document.getElementById('unread-' + chatId).innerHTML = '';
            }
            else {
                chatlistItems[i].className = "chatList-item"
            }
        }
    });
    $('.form-check-input').click(function () {
        let state = $(this).val();
        if (state == 'True') {
            $(this).val('False');
            $(this).removeAttr('checked');
            //Smart Reply State değişecek
        }
        else {
            $(this).val('True');
            $(this).attr('checked', 'checked');
            //Smart Reply State değişecek
        }
    })
});
