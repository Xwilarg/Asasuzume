<?php

require_once "vendor/autoload.php";

use Twig\Loader\FilesystemLoader;
use Twig\Environment;

$loader = new FilesystemLoader("templates");
$twig = new Environment($loader);

$data = json_decode(file_get_contents("assets/data.json"), true);
if ($_GET["name"] !== "") {
    $elem = current(array_filter($data, function($x) {
        return $x["name"] == $_GET["name"];
    }));
    if ($elem) {
        echo $twig->render("character.html.twig", [
            "data" => $elem,
            "audio" => [
                "bond1", "bond2", "bond3", "bond4", "bond5",
                "lobby1", "lobby2", "lobby3", "lobby4", "lobby5", "lobby6", "lobby7", "lobby8",
                "login_normal", "login_max"
            ]
        ]);
        return;
    }
}

echo $twig->render("index.html.twig", [
    "data" => $data
]);