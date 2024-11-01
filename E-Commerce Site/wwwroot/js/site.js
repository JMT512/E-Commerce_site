bNext = document.getElementById("next")
bPrevious = document.getElementById("previous")

let imgCurrent = 0

let images = ['Assets/image1.jpg', 'Assets/image2.jpg', 'Assets/image3.jpg', 'Assets/image4.jpg', 'Assets/image5.jpg']

function displayImage() {
    if (imgCurrent > images.length - 1) { imgCurrent = 0 }
    myImage.src = images[imgCurrent];
}

bNext.onclick = function displayNext() {
    if (imgCurrent >= images.length - 1) {
        imgCurrent = 0
        myImage.src = images[imgCurrent];
    }
    else {
        imgCurrent = imgCurrent + 1;
        myImage.src = images[imgCurrent];
    }

}

bPrevious.onclick = function displayPrevious() {

    if (imgCurrent == 0) {
        imgCurrent = 3;
        myImage.src = images[imgCurrent];
    }
    else {
        imgCurrent = imgCurrent - 1;
        myImage.src = images[imgCurrent];
    }

}

function displayForm(n) {
    //const num = n;
    switch (n) {
        case 1:
            document.getElementById("UpdateName").style.display = 'block';
            break;
        case 2:
            document.getElementById("UpdatePrice").style.display = 'block';
            break;
        case 3:
            document.getElementById("UpdateDescription").style.display = 'block';
            break;
        case 4:
            document.getElementById("UpdateImage").style.display = 'block';
            break;
        default:
            console.log("broken");
    };
}