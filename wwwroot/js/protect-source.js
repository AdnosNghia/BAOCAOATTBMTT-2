(function () {
    // Chặn các phím tắt phổ biến
    document.onkeydown = function (e) {
        // Chặn F12
        if (e.keyCode === 123) {
            return false;
        }

        // Chặn Ctrl+Shift+I, Ctrl+Shift+J, Ctrl+Shift+C
        if (e.ctrlKey && e.shiftKey && (e.keyCode === 73 || e.keyCode === 74 || e.keyCode === 67)) {
            return false;
        }

        // Chặn Ctrl+U
        if (e.ctrlKey && e.keyCode === 85) {
            return false;
        }

        // Chặn Ctrl+S
        if (e.ctrlKey && e.keyCode === 83) {
            return false;
        }
    };

    //// Chặn chuột phải
    document.oncontextmenu = function (e) {
        e.preventDefault();
    };

    // Chặn DevTools
    function detectDevTools() {
        if (window.devtools.isOpen) {
            // Có thể thay đổi hành động này tùy theo nhu cầu
            document.body.innerHTML = "DevTools đã bị phát hiện!";
        }
    }

    // Kiểm tra DevTools mỗi 100ms
    setInterval(detectDevTools, 100);

    // Vô hiệu hóa console
    console.log = console.warn = console.error = function () { };

    // Thêm một lớp bảo vệ khác
    setInterval(function () {
        debugger;
    }, 100);
})();