window.addEventListener("load", (_) => {
    const audio = document.querySelector("audio");
    for (const a of document.querySelectorAll("td > a")) {
        console.log(a);
        a.addEventListener("click", (e) => {
            e.preventDefault();
            if (!audio.paused && audio.src == a.href) { // Current audio already playing so we pause it
                audio.pause();
            } else {
                audio.src = a.href;
                audio.play();
            }
        })
    }
})