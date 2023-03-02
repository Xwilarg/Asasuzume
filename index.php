<?php

require_once "vendor/autoload.php";

use Twig\Loader\FilesystemLoader;
use Twig\Environment;

$loader = new FilesystemLoader("templates");
$twig = new Environment($loader);

$data = [];
$targetName = $_GET["name"];
foreach (new DirectoryIterator('./assets/data') as $fileInfo) {
    if ($fileInfo->isDot()) continue;

    $content = json_decode(file_get_contents("./assets/data/" . $fileInfo), true);
    if (substr($fileInfo, 0, -5) === $targetName) {
        echo $twig->render("character.html.twig", [
            "data" => $content,
            "audio" => [
                "bond1" => "Bond Lvl 1",
                "bond2" => "Bond Lvl 2",
                "bond3" => "Bond Lvl 3",
                "bond4" => "Bond Lvl 4",
                "bond5" => "Bond Lvl 5",
                "lobby1" => "Lobby 1",
                "lobby2" => "Lobby 2",
                "lobby3" => "Lobby 3",
                "lobby4" => "Lobby 4",
                "lobby5" => "Lobby 5",
                "lobby6" => "Lobby 6",
                "lobby7" => "Lobby 7",
                "login_normal" => "Login",
                "login_max" => "Login (Max Bond)",
                "gift_normal" => "Gift",
                "gift_preferred" => "Gift (Preferred)",
                "first" => "First Place",
                "obtained" => "Obtained"
            ]
        ]);
        return;
    }
    array_push($data, $content);
}

echo $twig->render("index.html.twig", [
    "data" => $data
]);