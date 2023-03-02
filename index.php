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
                "bond1", "bond2", "bond3", "bond4", "bond5",
                "lobby1", "lobby2", "lobby3", "lobby4", "lobby5", "lobby6", "lobby7", "lobby8",
                "login_normal", "login_max"
            ]
        ]);
        return;
    }
    array_push($data, $content);
}

echo $twig->render("index.html.twig", [
    "data" => $data
]);