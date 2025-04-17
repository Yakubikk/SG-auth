import GoToUserPageButton from "@/components/blocks/main-page/go-to-user-page-button";
import GoToAdminPageButton from "@/components/blocks/main-page/go-to-admin-page-button";

export default function Home() {
    return (
        <div>
            Main page
            <GoToUserPageButton />
            <GoToAdminPageButton />
        </div>
    );
}
