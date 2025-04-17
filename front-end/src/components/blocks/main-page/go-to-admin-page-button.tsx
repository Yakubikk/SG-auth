import Link from "next/link";
import {Button} from "@/components";

const GoToAdminPageButton = () => {
    return (
        <Link href="/admin">
            <Button
                variant='outlined'
                size='lg'
            >
                Перейти на страницу администратора
            </Button>
        </Link>
    );
}

export {GoToAdminPageButton};
export default GoToAdminPageButton;
