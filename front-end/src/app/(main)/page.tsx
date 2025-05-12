import GoToUserPageButton from "@/components/blocks/main-page/go-to-user-page-button";
import GoToAdminPageButton from "@/components/blocks/main-page/go-to-admin-page-button";
import withAuth, {WithAuthProps} from "@/services/with-auth";

function Home({ user }: WithAuthProps) {
    return (
        <div>
            {`Main page of ${user.userName}`}
            <GoToUserPageButton />
            <GoToAdminPageButton />
        </div>
    );
}

export default withAuth(Home);
