﻿const threshold = 10;
let seconds = 0;
let clicks = 0;
const currentScoreElement = document.getElementById("current_score");
const recordScoreElement = document.getElementById("record_score");
const profitPerClickElement = document.getElementById("profit_per_click");
const profitPerSecondElement = document.getElementById("profit_per_second");
let currentScore = Number(currentScoreElement.innerText);
let recordScore = Number(recordScoreElement.innerText);
let profitPerSecond = Number(profitPerSecondElement.innerText);
let profitPerClick = Number(profitPerClickElement.innerText);


$(document).ready(function () {
    const clickitem = document.getElementById("clickitem");

    clickitem.onclick = click;
    setInterval(addSecond, 1000)

    const boostButtons = document.getElementsByClassName("boost-button");
    const supportButtons = document.getElementsByClassName("support-button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        boostButton.onclick = () => boostButtonClick(boostButton);
    }

    for (let i = 0; i < supportButtons.length; i++) {
        const supportButton = supportButtons[i];

        supportButton.onclick = () => supportButtonClick(supportButton);
    }

    toggleBoostsAvailability();
    toggleSupportsAvailability();
})

function boostButtonClick(boostButton) {
    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buyBoost(boostButton);
}

function buyBoost(boostButton) {
    const boostIdElement = boostButton.getElementsByClassName("boost-id")[0];
    const boostId = boostIdElement.innerText;

    $.ajax({
        url: '/boost/buy',
        method: 'post',
        dataType: 'json',
        data: { boostId: boostId },
        success: (response) => onBuyBoostSuccess(response, boostButton),
    });
}

function onBuyBoostSuccess(response, boostButton) {
    const score = response["score"];

    const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
    const boostQuantityElement = boostButton.getElementsByClassName("boost-quantity")[0];

    const boostPrice = Number(response["price"]);
    const boostQuantity = Number(response["quantity"]);

    boostPriceElement.innerText = boostPrice;
    boostQuantityElement.innerText = boostQuantity;

    updateScoreFromApi(score);
}




function supportButtonClick(supportButton) {
    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }
    buySupport(supportButton);
}

function buySupport(supportButton) {
    const supportIdElement = supportButton.getElementsByClassName("support-id")[0];
    const supportId = supportIdElement.innerText;

    $.ajax({
        url: '/support/buy',
        method: 'post',
        dataType: 'json',
        data: { supportId: supportId },
        success: (response) => onBuySupportSuccess(response, supportButton),
    });
}

function onBuySupportSuccess(response, supportButton) {
    const score = response["score"];

    const supportPriceElement = supportButton.getElementsByClassName("support-price")[0];
    const supportQuantityElement = supportButton.getElementsByClassName("support-quantity")[0];

    const supportPrice = Number(response["price"]);
    const supportQuantity = Number(response["quantity"]);

    supportPriceElement.innerText = supportPrice;
    supportQuantityElement.innerText = supportQuantity;

    updateScoreFromApi(score);
}

function addSecond() {
    seconds++;

    if (seconds >= threshold) {
        addPointsToScore();
    }

    if (seconds > 0) {
        addPointsFromSecond();
    }
}

function click() {
    clicks++;

    if (clicks >= threshold) {
        addPointsToScore();
    }

    if (clicks > 0) {
        addPointsFromClick();
    }
}

function updateScoreFromApi(scoreData) {
    currentScore = Number(scoreData["currentScore"]);
    recordScore = Number(scoreData["recordScore"]);
    profitPerClick = Number(scoreData["profitPerClick"]);
    profitPerSecond = Number(scoreData["profitPerSecond"]);

    updateUiScore();
}

function updateUiScore() {
    currentScoreElement.innerText = currentScore;
    recordScoreElement.innerText = recordScore;
    profitPerClickElement.innerText = profitPerClick;
    profitPerSecondElement.innerText = profitPerSecond;

    toggleBoostsAvailability();
    toggleSupportsAvailability();
}

function addPointsFromClick() {
    currentScore += profitPerClick;
    recordScore += profitPerClick;

    updateUiScore();
}

function addPointsFromSecond() {
    currentScore += profitPerSecond;
    recordScore += profitPerSecond;

    updateUiScore();
}

function addPointsToScore() {
    $.ajax({
        url: '/score',
        method: 'post',
        dataType: 'json',
        data: { clicks: clicks, seconds: seconds },
        success: (response) => onAddPointsSuccess(response),
    });
}

function onAddPointsSuccess(response) {
    seconds = 0;
    clicks = 0;

    updateScoreFromApi(response);
}

function toggleBoostsAvailability() {
    const boostButtons = document.getElementsByClassName("boost-button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        const boostPriceElement = boostButton.getElementsByClassName("boost-price")[0];
        const boostPrice = Number(boostPriceElement.innerText);

        if (boostPrice > currentScore) {
            boostButton.disabled = true;
            continue;
        }

        boostButton.disabled = false;
    }
}

function toggleSupportsAvailability() {
    const supportButtons = document.getElementsByClassName("support-button");

    for (let i = 0; i < supportButtons.length; i++) {
        const supportButton = supportButtons[i];

        const supportPriceElement = supportButton.getElementsByClassName("support-price")[0];
        const supportPrice = Number(supportPriceElement.innerText);

        if (supportPrice > currentScore) {
            supportButton.disabled = true;
            continue;
        }

        supportButton.disabled = false;
    }
}