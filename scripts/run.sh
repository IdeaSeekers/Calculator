if [[ -z "${CALCULATOR_BACKEND_PATH}" ]]; then
	echo CALCULATOR_BACKEND_PATH not set
	exit
fi

docker build $CALCULATOR_BACKEND_PATH -t calculator_image
docker run --privileged --name calculator_container calculator_image bash /calculator/scripts/init.sh
