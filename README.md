# CongratulationsGenerator
A small app that can help you to generate wishes for your friends!

## Overview

The app generates a new single file based on a provided template. This files is populated with recipients' names and wishes for them, three for each recipient. Triples of wishes are unique; no triple will be generated for two different people in the same run. Moreover, the app will try to avoid using wishes several times, prefering new wishes to already used ones. Wishes are selected from three categories (one from each category).

## What it can do?

- generate unique triples of wishes
- cover all wishes and do not use any wish more than one time (if possible)
- predict whether there are enough wishes
- change output document font
- read preferred font from config
- read template path from config
- create many output files with different names
- recreate output folder if it doesn't exist
- show message about success or failure
