import Link from "next/link";
import {Button} from "@/components";

const GoToUserPageButton = () => {
    return (
        <Link href="/user">
            <Button
                variant='outlined'
                size='lg'
            >
                Перейти на страницу пользователя
            </Button>
        </Link>
    );
}

export {GoToUserPageButton};
export default GoToUserPageButton;
