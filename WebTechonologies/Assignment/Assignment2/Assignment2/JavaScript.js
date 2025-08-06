/* Question 4 */
function restartAnimation() {
    const box = document.getElementById('box');
    box.style.animation = 'none'; // remove animation
    void box.offsetWidth; // force reflow
    box.style.animation = 'slide 2s ease forwards';
    box.style.animationDelay = '2s';
}