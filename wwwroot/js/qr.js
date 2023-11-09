window.addEventListener("load", () => {
    const qrCodeDataElement = document.getElementById("qrCodeData");

    if (qrCodeDataElement) {
        const uri = qrCodeDataElement.getAttribute('data-url');

        if (uri) {
            new QRCode(document.getElementById("qrCode"), {
                text: uri,
                width: 150,
                height: 150
            });
        } else {
            console.error("QR code data URL is missing.");
        }
    } else {
        console.error("QR code data element not found.");
    }
});
