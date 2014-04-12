(function() {
  this.Maze = (function() {
    var changeObject, createMaze, createMazeObjectHandler, createMazeObjectHandlers, loadMap, maze, mazeObjectSize, mazeToJson, robotToJson, setRobotInMaze, setRobotPosition, startExplore;

    function Maze() {}

    maze = $("#maze");

    mazeObjectSize = 10;

    createMaze = function(jsonMaze) {
      var html, lastY, mazeHeight, mazeObjectCount, mazeObjectPerLine, mazeTrack, mazeWidth, num, objectX, objectY, trackItem, _i, _ref;
      this.maze.empty();
      if (jsonMaze !== void 0) {
        mazeWidth = jsonMaze.width;
        mazeHeight = jsonMaze.height;
        mazeTrack = jsonMaze.track;
        $("#txtMazeWidth").val(mazeWidth);
        $("#txtMazeHeight").val(mazeHeight);
      } else {
        mazeWidth = $("#txtMazeWidth").val();
        mazeHeight = $("#txtMazeHeight").val();
        mazeTrack = null;
      }
      mazeObjectPerLine = parseInt(mazeWidth) - 1;
      mazeObjectCount = mazeWidth * mazeHeight;
      trackItem = 0;
      objectX = 0;
      objectY = 0;
      lastY = 0;
      html = "<div class='line'>";
      for (num = _i = 0, _ref = mazeObjectCount - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; num = 0 <= _ref ? ++_i : --_i) {
        if (objectY !== lastY) {
          lastY = objectY;
          if (lastY > 0) {
            html += "</div>";
          }
          html += "<div class='line'>";
        }
        if (mazeTrack !== null && mazeTrack[trackItem].x === objectX && mazeTrack[trackItem].y === objectY) {
          html += "<div class='mazeObject space' data-x='" + objectX + "' data-y='" + objectY + "'></div>";
          if (mazeTrack.length > trackItem + 1) {
            trackItem++;
          }
        } else {
          html += "<div class='mazeObject wall' data-x='" + objectX + "' data-y='" + objectY + "'></div>";
        }
        if (objectX === mazeObjectPerLine) {
          objectX = 0;
          objectY++;
        } else {
          objectX++;
        }
      }
      html += "</div>";
      return this.maze.append(html);
    };

    mazeToJson = function() {
      var a, objectX, objectY, objectsPerLine, returnJSON, txtMazeHeight, txtMazeWidth;
      txtMazeHeight = $("#txtMazeHeight").val();
      txtMazeWidth = $("#txtMazeWidth").val();
      objectsPerLine = parseInt(txtMazeWidth) - 1;
      objectY = 0;
      objectX = 0;
      returnJSON = '{';
      returnJSON += '    "width":' + txtMazeWidth;
      returnJSON += '    ,"height":' + txtMazeHeight;
      returnJSON += '    ,"track": [';
      $.each($(".mazeObject"), function() {
        var objectValue;
        if ($(this).hasClass("space")) {
          objectValue = "space";
          returnJSON += '{"x": ' + objectX + ', "y": ' + objectY + ' },';
        }
        if (objectX === objectsPerLine) {
          objectX = 0;
          return objectY++;
        } else {
          return objectX++;
        }
      });
      a = returnJSON.length;
      returnJSON = returnJSON.substring(0, a - 1);
      returnJSON += "    ]";
      returnJSON += "}";
      return returnJSON;
    };

    robotToJson = function() {
      var a, returnJSON;
      returnJSON = "[";
      $.each($(".robot"), function() {
        var id, x, y;
        id = $(this).attr("data-id");
        x = $(this).attr("data-x");
        y = $(this).attr("data-y");
        return returnJSON += '{"id":' + id + ', "x": ' + x + ', "y": ' + y + ' },';
      });
      a = returnJSON.length;
      returnJSON = returnJSON.substring(0, a - 1);
      returnJSON += "]";
      return returnJSON;
    };

    loadMap = function(mapName) {
      $.get("/Content/maze/" + mapName + ".html", function(data) {
        return createMaze(JSON.parse(data));
      });
      return void 0;
    };

    setRobotInMaze = function(robotCount) {
      var coordinate, html, num, randomSpace, robotX, robotY, trackCount, tracks, _i, _ref, _results;
      tracks = JSON.parse(mazeToJson()).track;
      trackCount = tracks.length;
      _results = [];
      for (num = _i = 0, _ref = robotCount - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; num = 0 <= _ref ? ++_i : --_i) {
        randomSpace = Math.floor(Math.random() * trackCount);
        robotX = tracks[randomSpace].x;
        robotY = tracks[randomSpace].y;
        coordinate = robotX + "-" + robotY;
        html = "<div class='mazeObject robot' data-x='" + robotX + "'  data-y='" + robotY + "' data-id='" + num + "'></div>";
        $("#maze").append(html);
        _results.push(setRobotPosition(num, robotX, robotY));
      }
      return _results;
    };

    changeObject = (function() {
      var landObject, _i, _len, _results;
      _results = [];
      for (_i = 0, _len = json.length; _i < _len; _i++) {
        landObject = json[_i];
        if (landObject.value === "space") {
          $(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").removeClass("wall");
          _results.push($(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").addClass("space"));
        } else {
          $(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").removeClass("space");
          _results.push($(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").addClass("wall"));
        }
      }
      return _results;
    });

    setRobotPosition = function(robotId, x, y) {
      var objectX, objectY;
      objectX = x * mazeObjectSize;
      objectY = y * mazeObjectSize;
      return $(".robot[data-id=" + robotId + "]").css('left', objectX + "px").css('top', objectY + "px");
    };

    startExplore = function() {
      var robots;
      maze = this.mazeToJson();
      robots = this.robotToJson();
      return this.hub.startExplore(maze, robots);
    };

    createMazeObjectHandlers = function() {
      return $(".mazeObject").click(function() {
        return createMazeObjectHandler(this);
      });
    };

    createMazeObjectHandler = function(obj) {
      if ($(obj).hasClass('wall')) {
        $(obj).removeClass('wall');
        return $(obj).addClass('space');
      } else if ($(obj).hasClass('space')) {
        $(obj).removeClass('space');
        return $(obj).addClass('wall');
      }
    };

    return Maze;

  })();

}).call(this);
